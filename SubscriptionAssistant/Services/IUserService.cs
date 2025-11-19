using SubscriptionAssistant.Models.DTO;

namespace SubscriptionAssistant.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO userDto);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDTO?> UpdateUserRoleAsync(int id, int roleId);
        Task<bool> UserExistsAsync(string email, string username);
    }
}