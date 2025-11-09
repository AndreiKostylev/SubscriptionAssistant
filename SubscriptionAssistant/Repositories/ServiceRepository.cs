using Microsoft.EntityFrameworkCore;
using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(SubscriptionAssistantDbContext context) : base(context) { }

        public async Task<Service?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<IEnumerable<Service>> GetPopularServicesAsync(int count)
        {
            return await _dbSet
                .OrderByDescending(s => s.Subscriptions.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(s => s.Subscriptions.Any(sub => sub.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<bool> ServiceExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(s => s.Name == name);
        }
    }
}
