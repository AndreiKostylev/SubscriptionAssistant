using Microsoft.EntityFrameworkCore;
using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(SubscriptionAssistantDbContext context) : base(context) { }

        public async Task<IEnumerable<Subscription>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(s => s.Category)
                .Include(s => s.Service)
                .Include(s => s.Payments)
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetActiveSubscriptionsAsync()
        {
            return await _dbSet
                .Include(s => s.User)
                .Include(s => s.Service)
                .Where(s => s.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetExpiringSubscriptionsAsync(int daysBeforeExpiry)
        {
            var targetDate = DateTime.UtcNow.AddDays(daysBeforeExpiry);
            return await _dbSet
                .Include(s => s.User)
                .Include(s => s.Service)
                .Where(s => s.IsActive && s.NextPaymentDate.Date <= targetDate.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Include(s => s.User)
                .Include(s => s.Service)
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetByServiceIdAsync(int serviceId)
        {
            return await _dbSet
                .Include(s => s.User)
                .Include(s => s.Category)
                .Where(s => s.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<Subscription?> GetSubscriptionWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(s => s.User)
                .Include(s => s.Category)
                .Include(s => s.Service)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<decimal> GetTotalMonthlyCostAsync(int userId)
        {
            return await _dbSet
                .Where(s => s.UserId == userId && s.IsActive && s.BillingCycle == "monthly")
                .SumAsync(s => s.Price);
        }

        public async Task<bool> DeactivateSubscriptionAsync(int id)
        {
            var subscription = await GetByIdAsync(id);
            if (subscription == null) return false;

            subscription.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
