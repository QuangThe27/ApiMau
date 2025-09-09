using ApiMau.DTOs;

namespace ApiMau.Services
{
    public interface IUserService
    {
        // Định nghĩa phương thức để lấy tất cả người dùng
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}