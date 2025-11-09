using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetBySubscriptionIdAsync(int subscriptionId);
        Task<IEnumerable<Payment>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Payment>> GetPaymentsInDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSpentByUserAsync(int userId);
        Task<decimal> GetTotalSpentByUserInPeriodAsync(int userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Payment>> GetFailedPaymentsAsync();
    }
}
