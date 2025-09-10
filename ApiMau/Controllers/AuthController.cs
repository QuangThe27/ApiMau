// Controllers/AuthController.cs
using ApiMau.DTOs;
using ApiMau.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace ApiMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto loginDto)
        {
            // Bước 1: Controller gọi Business Service (AuthService) để xác thực
            var user = await _authService.AuthenticateUser(loginDto.Email, loginDto.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng." });
            }

            // Bước 2: Controller gọi Token Service (ITokenService) để tạo token
            var token = _tokenService.GenerateJwtToken(user.Id.ToString(), user.Email, user.Role);

            return Ok(new { token });
        }
    }
}