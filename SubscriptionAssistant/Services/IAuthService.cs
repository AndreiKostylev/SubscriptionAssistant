using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Models.SubscriptionManager.Models;

namespace SubscriptionAssistant.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);

        /// <summary>
        /// Генерация JWT токена
        /// </summary>
        string GenerateJwtToken(User user);
    }
}
