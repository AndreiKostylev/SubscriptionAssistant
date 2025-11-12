using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Services;

namespace SubscriptionAssistant.Controllers
{
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
        public async Task<ActionResult<IEnumerable<SubscriptionDTO>>> GetUserSubscriptions(int userId)
        {
            var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
            return Ok(subscriptions);
        }

        /// <summary>
        /// Получить подписку по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionDTO>> GetSubscription(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);

            if (subscription == null)
            {
                return NotFound(new
                {
                    title = "Not Found",
                    status = 404,
                    detail = $"Подписка с ID {id} не найдена.",
                    instance = $"/api/subscriptions/{id}"
                });
            }

            return Ok(subscription);
        }

        /// <summary>
        /// Создать новую подписку
        /// </summary>
        [HttpPost("user/{userId}")]
        public async Task<ActionResult<SubscriptionDTO>> CreateSubscription(int userId, [FromBody] CreateSubscriptionDTO subscriptionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    title = "Bad Request",
                    status = 400,
                    detail = "Ошибки валидации",
                    errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }

            try
            {
                var createdSubscription = await _subscriptionService.CreateSubscriptionAsync(subscriptionDto, userId);
                return CreatedAtAction(nameof(GetSubscription), new { id = createdSubscription.Id }, createdSubscription);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    title = "Bad Request",
                    status = 400,
                    detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Деактивировать подписку
        /// </summary>
        [HttpPut("{id}/deactivate")]
        public async Task<ActionResult> DeactivateSubscription(int id)
        {
            var result = await _subscriptionService.DeactivateSubscriptionAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    title = "Not Found",
                    status = 404,
                    detail = $"Подписка с ID {id} не найдена.",
                    instance = $"/api/subscriptions/{id}/deactivate"
                });
            }

            return NoContent();
        }

        /// <summary>
        /// Удалить подписку по ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            var result = await _subscriptionService.DeleteSubscriptionAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    title = "Not Found",
                    status = 404,
                    detail = $"Подписка с ID {id} не найдена.",
                    instance = $"/api/subscriptions/{id}"
                });
            }

            return NoContent();
        }

        /// <summary>
        /// Получить подписки, у которых скоро закончится оплата
        /// </summary>
        [HttpGet("expiring/{days}")]
        public async Task<ActionResult<IEnumerable<SubscriptionDTO>>> GetExpiringSubscriptions(int days)
        {
            var subscriptions = await _subscriptionService.GetExpiringSubscriptionsAsync(days);
            return Ok(subscriptions);
        }

        /// <summary>
        /// Обновить подписку
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<SubscriptionDTO>> UpdateSubscription(int id, [FromBody] UpdateSubscriptionDTO subscriptionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    title = "Bad Request",
                    status = 400,
                    detail = "Ошибки валидации",
                    errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }

            var updatedSubscription = await _subscriptionService.UpdateSubscriptionAsync(id, subscriptionDto);

            if (updatedSubscription == null)
            {
                return NotFound(new
                {
                    title = "Not Found",
                    status = 404,
                    detail = $"Подписка с ID {id} не найдена.",
                    instance = $"/api/subscriptions/{id}"
                });
            }

            return Ok(updatedSubscription);
        }
    }
}
