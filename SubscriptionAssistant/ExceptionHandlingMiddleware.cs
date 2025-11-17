using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace SubscriptionAssistant
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла необработанная ошибка");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is SecurityTokenException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error = "Неверный токен",
                    message = exception.Message,
                    timestamp = DateTime.UtcNow
                }));
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new
            {
                error = "Внутренняя ошибка сервера",
                message = exception.Message,
                timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
