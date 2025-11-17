using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Services;

namespace SubscriptionAssistant.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// Получить все подписки пользователя
        /// </summary>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SubscriptionDTO>>> GetUserSubscriptions(int userId)
        {
            var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
            return Ok(subscriptions);
        }

        /// <summary>
        /// Получить подписку по ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SubscriptionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubscriptionDTO>> GetSubscription(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);

            if (subscription == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Status = 404,
                    Detail = $"Подписка с ID {id} не найдена.",
                    Instance = $"/api/subscriptions/{id}"
                });
            }

            return Ok(subscription);
        }

        /// <summary>
        /// Создать новую подписку
        /// </summary>
        [HttpPost("user/{userId}")]
        [ProducesResponseType(typeof(SubscriptionDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubscriptionDTO>> CreateSubscription(int userId, [FromBody] CreateSubscriptionDTO subscriptionDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                {
                    Title = "Bad Request",
                    Status = 400,
                    Detail = "Ошибки валидации"
                });
            }

            var createdSubscription = await _subscriptionService.CreateSubscriptionAsync(subscriptionDto, userId);
            return CreatedAtAction(nameof(GetSubscription), new { id = createdSubscription.Id }, createdSubscription);
        }

        /// <summary>
        /// Деактивировать подписку
        /// </summary>
        [HttpPut("{id}/deactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeactivateSubscription(int id)
        {
            var result = await _subscriptionService.DeactivateSubscriptionAsync(id);

            if (!result)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Status = 404,
                    Detail = $"Подписка с ID {id} не найдена.",
                    Instance = $"/api/subscriptions/{id}/deactivate"
                });
            }

            return NoContent();
        }

        /// <summary>
        /// Удалить подписку по ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            var result = await _subscriptionService.DeleteSubscriptionAsync(id);

            if (!result)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Status = 404,
                    Detail = $"Подписка с ID {id} не найдена.",
                    Instance = $"/api/subscriptions/{id}"
                });
            }

            return NoContent();
        }

        /// <summary>
        /// Получить подписки, у которых скоро закончится оплата
        /// </summary>
        [HttpGet("expiring/{days}")]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SubscriptionDTO>>> GetExpiringSubscriptions(int days)
        {
            var subscriptions = await _subscriptionService.GetExpiringSubscriptionsAsync(days);
            return Ok(subscriptions);
        }

        /// <summary>
        /// Обновить подписку
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SubscriptionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubscriptionDTO>> UpdateSubscription(int id, [FromBody] UpdateSubscriptionDTO subscriptionDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                {
                    Title = "Bad Request",
                    Status = 400,
                    Detail = "Ошибки валидации"
                });
            }

            var updatedSubscription = await _subscriptionService.UpdateSubscriptionAsync(id, subscriptionDto);

            if (updatedSubscription == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Status = 404,
                    Detail = $"Подписка с ID {id} не найдена.",
                    Instance = $"/api/subscriptions/{id}"
                });
            }

            return Ok(updatedSubscription);
        }
    }
}