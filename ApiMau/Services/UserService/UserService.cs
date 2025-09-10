using ApiMau.Data;
using ApiMau.DTOs;
using ApiMau.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMau.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Lấy danh sách tất cả người dùng và chuyển đổi sang UserDto.
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            // Truy cập database qua DbContext để lấy danh sách User
            var users = await _dbContext.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    Password = u.Password 
                })
                .ToListAsync();

            // Trả về danh sách UserDto đã được ánh xạ
            return users;
        }

        // Create a new user
        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            // Chuyển đổi từ DTO sang entity User
            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                Role = userDto.Role,
                Password = userDto.Password
            };

            // Thêm đối tượng User vào DbContext
            _dbContext.Users.Add(user);

            // Lưu thay đổi vào database
            await _dbContext.SaveChangesAsync();

            // Cập nhật DTO với Id mới được tạo và trả về
            userDto.Id = user.Id;
            return userDto;
        }

        // Xóa người dùng theo ID
        public async Task<bool> DeleteUserAsync(int userId)
        {
            // Tìm người dùng trong database bằng ID
            var userToDelete = await _dbContext.Users.FindAsync(userId);

            // Kiểm tra xem người dùng có tồn tại không
            if (userToDelete == null)
            {
                return false; // Trả về false nếu không tìm thấy người dùng
            }

            // Xóa người dùng khỏi DbContext
            _dbContext.Users.Remove(userToDelete);

            // Lưu thay đổi vào database
            await _dbContext.SaveChangesAsync();

            // Trả về true nếu xóa thành công
            return true;
        }

        public async Task<UserDto?> UpdateUserAsync(int userId, UpdateUserDto userDto)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            // Ánh xạ các trường từ DTO vào đối tượng User
            if (userDto.FullName != null)
            {
                user.FullName = userDto.FullName;
            }

            if (userDto.Email != null)
            {
                user.Email = userDto.Email;
            }

            if (userDto.Role != null)
            {
                user.Role = userDto.Role;
            }

            if (userDto.Password != null)
            {
                user.Password = userDto.Password;
            }

            await _dbContext.SaveChangesAsync();

            // Trả về DTO của người dùng đã cập nhật
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password // Hoặc trả về một DTO không có mật khẩu
            };
        }
    }
}