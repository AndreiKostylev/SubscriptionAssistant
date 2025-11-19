using AutoMapper;
using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Models.SubscriptionManager.Models;
using SubscriptionAssistant.Repositories;

namespace SubscriptionAssistant.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllWithRoleAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdWithRoleAsync(id);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO userDto)
        {
            if (await _userRepository.UserExistsAsync(userDto.Email, userDto.Username))
            {
                throw new InvalidOperationException("Пользователь с таким email или username уже существует");
            }

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.RoleId = 1; // Роль "User" по умолчанию

            var createdUser = await _userRepository.CreateAsync(user);
            var userWithRole = await _userRepository.GetByIdWithRoleAsync(createdUser.Id);
            return _mapper.Map<UserDTO>(userWithRole!);
        }

        public async Task<bool> UserExistsAsync(string email, string username)
        {
            return await _userRepository.UserExistsAsync(email, username);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserDTO?> UpdateUserRoleAsync(int id, int roleId)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            user.RoleId = roleId;
            user.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateAsync(user);
            var userWithRole = await _userRepository.GetByIdWithRoleAsync(updatedUser.Id);
            return _mapper.Map<UserDTO>(userWithRole);
        }
    }
}
