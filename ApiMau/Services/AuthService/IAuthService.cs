// Services/IAuthService.cs
using ApiMau.Models;

namespace ApiMau.Services.AuthService
{
    public interface IAuthService
    {
        Task<User?> AuthenticateUser(string email, string password);
    }
}