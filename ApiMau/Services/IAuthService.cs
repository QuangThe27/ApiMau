// Services/IAuthService.cs
using ApiMau.Models;
using System.Security.Claims;

namespace ApiMau.Services
{
    public interface IAuthService
    {
        Task<User> AuthenticateUser(string email, string password);
    }
}