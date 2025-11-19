using SubscriptionAssistant.Models.SubscriptionManager.Models;

namespace SubscriptionAssistant.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdWithRoleAsync(int id);
        Task<bool> UserExistsAsync(string email, string username);
        Task<IEnumerable<User>> GetUsersWithSubscriptionsAsync();
        Task<IEnumerable<User>> GetAllWithRoleAsync();
    }
}
