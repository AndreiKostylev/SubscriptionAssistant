using SubscriptionAssistant.Models.DTO;

namespace SubscriptionAssistant.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        Task<UserDTO?> GetUserByIdAsync(int id);

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        Task<UserDTO> CreateUserAsync(CreateUserDTO userDto);

        /// <summary>
        /// Проверить существование пользователя по email и username
        /// </summary>
        Task<bool> UserExistsAsync(string email, string username);
    }
}