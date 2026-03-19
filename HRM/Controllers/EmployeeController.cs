using Microsoft.AspNetCore.Mvc;
using HRM.Interfaces;
using HRM.DTOs;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetById(id);
            if (emp == null) return NotFound();

            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDTO dto)
        {
            var updated = await _service.Update(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}