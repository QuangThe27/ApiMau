using ApiMau.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMau.Controllers
{
    [Route("api/[controller]")]
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
    }
}