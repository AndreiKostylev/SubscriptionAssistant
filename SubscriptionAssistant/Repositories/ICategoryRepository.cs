using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetCategoriesWithSubscriptionsAsync();
        Task<IEnumerable<Category>> GetPopularCategoriesAsync(int count);
    }
}
