using Microsoft.AspNetCore.Mvc;
using HRM.Interfaces;
using HRM.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service) => _service = service;

        [HttpGet]
        [Authorize(Roles = "Admin,HR")] // 🔐 Chỉ sếp/nhân sự mới xem được toàn bộ danh sách
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        [Authorize] // ✅ Ai cũng có thể xem chi tiết (hoặc giới hạn chỉ chính họ)
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetById(id);
            if (emp == null) return NotFound(new { message = "Employee not found" });
            return Ok(emp);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,HR")] // 🔐 Chỉ Admin/HR mới thêm được nhân viên
        public async Task<IActionResult> Create([FromBody] EmployeeDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,HR")] // 🔐 Chặn nhân viên tự sửa hồ sơ của mình trái phép
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDTO dto)
        {
            var updated = await _service.Update(id, dto);
            if (!updated) return NotFound(new { message = "Employee not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // 🔐 Chỉ Admin mới có quyền đuổi việc (xóa) nhân viên
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound(new { message = "Employee not found" });
            return NoContent();
        }
    }
}