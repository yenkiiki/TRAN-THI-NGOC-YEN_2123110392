using HRM.DTOs;
using HRM.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;
        public AttendanceController(IAttendanceService service) => _service = service;

        [HttpPost("check-in")]
        [Authorize] // ✅ Employee dùng cái này
        public async Task<IActionResult> CheckIn()
        {
            var empId = GetEmployeeIdFromToken();
            if (empId == 0) return BadRequest(new { message = "Tài khoản chưa liên kết nhân viên" });
            var result = await _service.CheckInAsync(empId);
            return result.Contains("thành công") ? Ok(new { message = result }) : BadRequest(new { message = result });
        }

        [HttpPost("check-out")]
        [Authorize] // ✅ Employee dùng cái này
        public async Task<IActionResult> CheckOut()
        {
            var empId = GetEmployeeIdFromToken();
            if (empId == 0) return BadRequest(new { message = "Tài khoản chưa liên kết nhân viên" });
            var result = await _service.CheckOutAsync(empId);
            return result.Contains("thành công") ? Ok(new { message = result }) : BadRequest(new { message = result });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR")] // 🔐 Chỉ Admin/HR mới xem được bảng công tổng
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,HR")] // 🔐 Nhân viên không được tự sửa giờ làm
        public async Task<IActionResult> Update(int id, AttendanceUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result ? Ok(new { message = "Cập nhật thành công" }) : BadRequest("Cập nhật thất bại");
        }

        private int GetEmployeeIdFromToken()
        {
            var claim = User.FindFirst("EmployeeId")?.Value;
            return int.TryParse(claim, out int id) ? id : 0;
        }
    }
}