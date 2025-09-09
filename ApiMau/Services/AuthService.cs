// Services/AuthService.cs
using ApiMau.Data;
using ApiMau.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMau.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Phương thức này chỉ xác thực và trả về đối tượng User
        public async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            // TODO: So sánh mật khẩu đã hash ở đây
            if (user.Password != password)
                return null;

            return user;
        }
    }
}