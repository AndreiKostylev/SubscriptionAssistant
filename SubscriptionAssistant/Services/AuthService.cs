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

        public async Task<AuthResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequest.Login)
                      ?? await _userRepository.GetByUsernameAsync(loginRequest.Login);

            if (user == null || !VerifyPassword(loginRequest.Password, user.PasswordHash))
                return null;

            // Получаем пользователя с ролью
            var userWithRole = await _userRepository.GetByIdWithRoleAsync(user.Id);
            var token = GenerateJwtToken(userWithRole ?? user);
            var userDto = _mapper.Map<UserDTO>(userWithRole ?? user);

            return new AuthResponse
            {
                Token = token,
                Expires = DateTime.UtcNow.AddHours(2),
                User = userDto
            };
        }

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

            // Получаем пользователя с ролью
            var userWithRole = await _userRepository.GetByIdWithRoleAsync(createdUser.Id);
            var token = GenerateJwtToken(userWithRole ?? createdUser);
            var userDto = _mapper.Map<UserDTO>(userWithRole ?? createdUser);

            return new AuthResponse
            {
                Token = token,
                Expires = DateTime.UtcNow.AddHours(2),
                User = userDto
            };
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roleName = user.Role?.Name ?? "User";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName)
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

        public async Task<bool> HasAccessAsync(int userId, string requiredRole)
        {
            var user = await _userRepository.GetByIdWithRoleAsync(userId);
            if (user == null) return false;

            // Admin имеет доступ ко всему
            if (user.Role?.Name == "Admin") return true;

            return user.Role?.Name == requiredRole;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdWithRoleAsync(userId);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
