using HRM.DTOs;
using HRM.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🔐 Chặn mọi hành vi chấm công nếu chưa login
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [HttpPost("checkin/{employeeId}")]
        public async Task<IActionResult> CheckIn(int employeeId)
        {
            var result = await _service.CheckInAsync(employeeId);
            if (!result) return BadRequest(new { message = "Hôm nay bạn đã chấm công rồi hoặc nhân viên không tồn tại" });
            return Ok(new { message = "Check-in thành công!" });
        }

        [HttpPost("checkout/{employeeId}")]
        public async Task<IActionResult> CheckOut(int employeeId)
        {
            var result = await _service.CheckOutAsync(employeeId);
            if (!result) return BadRequest(new { message = "Không tìm thấy dữ liệu Check-in hoặc bạn đã Check-out rồi" });
            return Ok(new { message = "Check-out thành công!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AttendanceUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result) return BadRequest("Cập nhật thất bại");
            return Ok(new { message = "Cập nhật thành công" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(new { message = "Xóa dữ liệu thành công" });
        }
    }
}