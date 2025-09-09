// Services/IAuthService.cs
using ApiMau.Models;
using System.Security.Claims;

namespace ApiMau.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        Task<User> AuthenticateUser(string email, string password);
    }
}