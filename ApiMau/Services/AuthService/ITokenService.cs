// Services/ITokenService.cs
namespace ApiMau.Services.AuthService
{
    public interface ITokenService
    {
        string GenerateJwtToken(string id, string email, string role);
    }
}