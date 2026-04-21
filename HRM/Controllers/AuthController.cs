using HRM.DTOs;
using HRM.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service) => _service = service;

        [HttpPost("login")] // ✅ KHÔNG để Authorize vì cần login để lấy token
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.LoginAsync(dto);
            if (token == null) return Unauthorized("Invalid credentials");
            return Ok(new { token });
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")] // 🔐 Chỉ Admin mới được cấp account
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var result = await _service.RegisterAsync(dto);
                if (!result) return BadRequest(new { message = "Username đã tồn tại" });
                return Ok(new { message = "Tạo tài khoản thành công!" });
            }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] // 🔐 Chỉ Admin mới xem được danh sách tài khoản
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllUsersAsync());

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // 🔐 Chỉ Admin mới được xóa tài khoản
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUserAsync(id);
            if (!result) return NotFound();
            return Ok("Delete success");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // 🔐 Chỉ Admin mới được sửa tài khoản
        public async Task<IActionResult> Update(int id, RegisterDto dto)
        {
            var result = await _service.UpdateUserAsync(id, dto);
            if (!result) return NotFound();
            return Ok("Update success");
        }
    
}
}