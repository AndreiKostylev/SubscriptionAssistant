using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Models.SubscriptionManager.Models;

namespace SubscriptionAssistant.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);
        Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);
        string GenerateJwtToken(User user);
        Task<bool> HasAccessAsync(int userId, string requiredRole);
        Task<User?> GetUserByIdAsync(int userId);
    }
}
