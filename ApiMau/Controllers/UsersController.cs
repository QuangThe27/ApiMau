using ApiMau.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiMau.Services.UserService;

namespace ApiMau.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "admin")] // Chỉ cho phép người dùng có role "admin" truy cập
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng.
        /// Yêu cầu xác thực và phải có quyền admin.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            // Controller gọi tầng service để xử lý logic
            var users = await _userService.GetAllUsersAsync();

            // Trả về kết quả
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gọi tầng service để xử lý logic tạo người dùng
            var createdUser = await _userService.CreateUserAsync(userDto);

            // Trả về kết quả 201 Created và thông tin người dùng đã tạo
            return CreatedAtAction(nameof(GetAllUsers), new { id = createdUser.Id }, createdUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Gọi tầng service để xử lý logic xóa
            var isDeleted = await _userService.DeleteUserAsync(id);

            // Kiểm tra kết quả từ service
            if (!isDeleted)
            {
                // Trả về 404 Not Found nếu người dùng không tồn tại
                return NotFound($"User with ID {id} not found.");
            }

            // Trả về 204 No Content nếu xóa thành công
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, userDto);

            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(updatedUser);
        }
    }
}