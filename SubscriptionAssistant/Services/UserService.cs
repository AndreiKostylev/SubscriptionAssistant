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

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        public async Task<UserDTO> CreateUserAsync(CreateUserDTO userDto)
        {
            if (await _userRepository.UserExistsAsync(userDto.Email, userDto.Username))
            {
                throw new InvalidOperationException("Пользователь с таким email или username уже существует");
            }

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserDTO>(createdUser);
        }

        /// <summary>
        /// Проверить существование пользователя по email и username
        /// </summary>
        public async Task<bool> UserExistsAsync(string email, string username)
        {
            return await _userRepository.UserExistsAsync(email, username);
        }
    }
}
