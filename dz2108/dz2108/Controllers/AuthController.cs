using Microsoft.AspNetCore.Mvc;
using System;

namespace dz2108.Controllers
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest(new { Message = "Некоректні дані користувача!" });
            }

            Console.WriteLine($"[СЕРВЕР]: Зареєстровано користувача -> {user.Username} ({user.Email})");

            return Ok(new
            {
                Status = "Success",
                Message = $"Користувача '{user.Username}' успішно зареєстровано!",
                RegisteredAt = DateTime.Now
            });
        }
    }
}