// Services/ITokenService.cs
namespace ApiMau.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(string id, string email, string role);
    }
}