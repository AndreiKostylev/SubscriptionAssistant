using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Services;

namespace SubscriptionAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    title = "Not Found",
                    status = 404,
                    detail = $"Пользователь с ID {id} не найден.",
                    instance = $"/api/users/{id}"
                });
            }

            return Ok(user);
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    title = "Bad Request",
                    status = 400,
                    detail = "Ошибки валидации",
                    errors = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }

            try
            {
                var createdUser = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    title = "Bad Request",
                    status = 400,
                    detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Удалить пользователя по ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    title = "Not Found",
                    status = 404,
                    detail = $"Пользователь с ID {id} не найден.",
                    instance = $"/api/users/{id}"
                });
            }

            return NoContent();
        }
    }
}

