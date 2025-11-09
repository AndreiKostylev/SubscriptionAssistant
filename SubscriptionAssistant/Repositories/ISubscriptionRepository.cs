using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<IEnumerable<Subscription>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Subscription>> GetActiveSubscriptionsAsync();
        Task<IEnumerable<Subscription>> GetExpiringSubscriptionsAsync(int daysBeforeExpiry);
        Task<IEnumerable<Subscription>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Subscription>> GetByServiceIdAsync(int serviceId);
        Task<Subscription?> GetSubscriptionWithDetailsAsync(int id);
        Task<decimal> GetTotalMonthlyCostAsync(int userId);
        Task<bool> DeactivateSubscriptionAsync(int id);
    }
}
