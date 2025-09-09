// Services/AuthService.cs
using ApiMau.Data;
using ApiMau.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ApiMau.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;

        public AuthService(IConfiguration config, AppDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            // TODO: So sánh mật khẩu đã hash ở đây
            // if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            //     return null;

            // Tạm thời so sánh plain-text 
            if (user.Password != password)
                return null;

            return user;
        }
    }
}