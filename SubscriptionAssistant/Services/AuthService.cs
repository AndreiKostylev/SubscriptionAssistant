using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Models.SubscriptionManager.Models;
using SubscriptionAssistant.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SubscriptionAssistant.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        public async Task<AuthResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequest.Login)
                      ?? await _userRepository.GetByUsernameAsync(loginRequest.Login);

            if (user == null || !VerifyPassword(loginRequest.Password, user.PasswordHash))
                return null;

            var token = GenerateJwtToken(user);
            var userDto = _mapper.Map<UserDTO>(user);

            return new AuthResponse
            {
                Token = token,
                Expires = DateTime.UtcNow.AddHours(2),
                User = userDto
            };
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            if (await _userRepository.UserExistsAsync(registerRequest.Email, registerRequest.Username))
                throw new InvalidOperationException("Пользователь с таким email или username уже существует");

            var user = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                RoleId = 1, // Роль "User" по умолчанию
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);
            var token = GenerateJwtToken(createdUser);
            var userDto = _mapper.Map<UserDTO>(createdUser);

            return new AuthResponse
            {
                Token = token,
                Expires = DateTime.UtcNow.AddHours(2),
                User = userDto
            };
        }

        /// <summary>
        /// Генерация JWT токена
        /// </summary>
        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
