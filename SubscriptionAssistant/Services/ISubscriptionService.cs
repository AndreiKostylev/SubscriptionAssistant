using SubscriptionAssistant.Models.DTO;

namespace SubscriptionAssistant.Services
{
    public interface ISubscriptionService
    {
        /// <summary>
        /// Получить все подписки пользователя
        /// </summary>
        Task<IEnumerable<SubscriptionDTO>> GetUserSubscriptionsAsync(int userId);

        /// <summary>
        /// Получить подписку по ID
        /// </summary>
        Task<SubscriptionDTO?> GetSubscriptionByIdAsync(int id);

        /// <summary>
        /// Создать новую подписку
        /// </summary>
        Task<SubscriptionDTO> CreateSubscriptionAsync(CreateSubscriptionDTO subscriptionDto, int userId);

        /// <summary>
        /// Деактивировать подписку
        /// </summary>
        Task<bool> DeactivateSubscriptionAsync(int id);

        /// <summary>
        /// Получить подписки, у которых скоро закончится оплата
        /// </summary>
        Task<IEnumerable<SubscriptionDTO>> GetExpiringSubscriptionsAsync(int daysBeforeExpiry);

        /// <summary>
        /// Удалить подписку по ID
        /// </summary>
        Task<bool> DeleteSubscriptionAsync(int id);

        /// <summary>
        /// Обновить подписку
        /// </summary>
        Task<SubscriptionDTO?> UpdateSubscriptionAsync(int id, UpdateSubscriptionDTO subscriptionDto);
    }
}
