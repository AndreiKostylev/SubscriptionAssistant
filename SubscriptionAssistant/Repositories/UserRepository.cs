using Microsoft.EntityFrameworkCore;
using SubscriptionAssistant.Models;
using SubscriptionAssistant.Models.SubscriptionManager.Models;

namespace SubscriptionAssistant.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SubscriptionAssistantDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UserExistsAsync(string email, string username)
        {
            return await _dbSet.AnyAsync(u => u.Email == email || u.Username == username);
        }

        public async Task<IEnumerable<User>> GetUsersWithSubscriptionsAsync()
        {
            return await _dbSet
                .Include(u => u.Subscriptions)
                .ThenInclude(s => s.Service)
                .Include(u => u.Subscriptions)
                .ThenInclude(s => s.Category)
                .Where(u => u.Subscriptions.Any())
                .ToListAsync();
        }
    }
}
