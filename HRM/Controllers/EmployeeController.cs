using Microsoft.AspNetCore.Mvc;
using HRM.Interfaces;
using HRM.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔐 Bắt buộc đăng nhập cho tất cả API
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetById(id);

            if (emp == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(emp);
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.Update(id, dto);

            if (!updated)
                return NotFound(new { message = "Employee not found" });

            return NoContent();
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound(new { message = "Employee not found" });

            return NoContent();
        }
    }
}