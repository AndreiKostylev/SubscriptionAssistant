using SubscriptionAssistant.Models;

namespace SubscriptionAssistant.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<Service?> GetByNameAsync(string name);
        Task<IEnumerable<Service>> GetPopularServicesAsync(int count);
        Task<IEnumerable<Service>> GetServicesByCategoryAsync(int categoryId);
        Task<bool> ServiceExistsAsync(string name);
    }
}
