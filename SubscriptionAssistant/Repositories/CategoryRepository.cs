using Microsoft.EntityFrameworkCore;
using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SubscriptionAssistantDbContext context) : base(context) { }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithSubscriptionsAsync()
        {
            return await _dbSet
                .Include(c => c.Subscriptions)
                .ThenInclude(s => s.Service)
                .Include(c => c.Subscriptions)
                .ThenInclude(s => s.User)
                .Where(c => c.Subscriptions.Any())
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetPopularCategoriesAsync(int count)
        {
            return await _dbSet
                .OrderByDescending(c => c.Subscriptions.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}
