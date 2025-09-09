using ApiMau.Data;
using ApiMau.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiMau.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng và chuyển đổi sang UserDto.
        /// </summary>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            // Truy cập database qua DbContext để lấy danh sách User
            var users = await _dbContext.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();

            // Trả về danh sách UserDto đã được ánh xạ
            return users;
        }
    }
}