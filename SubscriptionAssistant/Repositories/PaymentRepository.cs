using Microsoft.EntityFrameworkCore;
using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(SubscriptionAssistantDbContext context) : base(context) { }

        public async Task<IEnumerable<Payment>> GetBySubscriptionIdAsync(int subscriptionId)
        {
            return await _dbSet
                .Where(p => p.SubscriptionId == subscriptionId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Service)
                .Where(p => p.Subscription.UserId == userId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsInDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(p => p.Subscription)
                .ThenInclude(s => s.User)
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Service)
                .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalSpentByUserAsync(int userId)
        {
            return await _dbSet
                .Include(p => p.Subscription)
                .Where(p => p.Subscription.UserId == userId && p.IsSuccessful)
                .SumAsync(p => p.Amount);
        }

        public async Task<decimal> GetTotalSpentByUserInPeriodAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(p => p.Subscription)
                .Where(p => p.Subscription.UserId == userId &&
                           p.IsSuccessful &&
                           p.PaymentDate >= startDate &&
                           p.PaymentDate <= endDate)
                .SumAsync(p => p.Amount);
        }

        public async Task<IEnumerable<Payment>> GetFailedPaymentsAsync()
        {
            return await _dbSet
                .Include(p => p.Subscription)
                .ThenInclude(s => s.User)
                .Where(p => !p.IsSuccessful)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }
    }
}
