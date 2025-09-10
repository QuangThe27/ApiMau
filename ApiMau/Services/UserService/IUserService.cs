using ApiMau.DTOs;

namespace ApiMau.Services.UserService
{
    public interface IUserService
    {
        // Định nghĩa phương thức để lấy tất cả người dùng
        Task<IEnumerable<UserDto>> GetAllUsersAsync(); 
        /* IEnumerable<T> biểu hiển phương thức này trả về 1 tập hợp các đối tượng mà bạn có thể lặp quá */

        // Định nghĩa phương thức để tạo người dùng mới
        Task<UserDto> CreateUserAsync(UserDto userDto);

        // Định nghĩa phương thức để xóa người dùng theo ID
        Task<bool> DeleteUserAsync(int userId);

        // Định nghĩa phương thức để cập nhật thông tin người dùng
        Task<UserDto?> UpdateUserAsync(int userId, UpdateUserDto userDto);
    }
}