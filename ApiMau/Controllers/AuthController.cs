// Controllers/AuthController.cs
using ApiMau.DTOs;
using ApiMau.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.AuthenticateUser(loginDto.Email, loginDto.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng." });
            }

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}