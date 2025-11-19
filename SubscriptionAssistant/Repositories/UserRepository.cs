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
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithRoleAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
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
                .Include(u => u.Role)
                .Where(u => u.Subscriptions.Any())
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithRoleAsync()
        {
            return await _dbSet
                .Include(u => u.Role)
                .ToListAsync();
        }
    }
}
