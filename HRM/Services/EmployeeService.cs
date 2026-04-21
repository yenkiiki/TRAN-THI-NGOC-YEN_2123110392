using HRM.Data;
using HRM.DTOs;
using HRM.Interfaces;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position) // 🔗 Join thêm bảng Position
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Email = e.Email,
                    Phone = e.Phone,
                    DateOfBirth = e.DateOfBirth,
                    Gender = e.Gender,
                    Address = e.Address,
                    HireDate = e.HireDate,
                    Status = e.Status,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : "N/A",
                    PositionId = e.PositionId,
                    PositionName = e.Position != null ? e.Position.PositionName : "N/A"
                })
                .ToListAsync();
        }

        public async Task<EmployeeDTO?> GetById(int id)
        {
            var emp = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (emp == null) return null;

            return new EmployeeDTO
            {
                Id = emp.Id,
                FullName = emp.FullName,
                Email = emp.Email,
                Phone = emp.Phone,
                DateOfBirth = emp.DateOfBirth,
                Gender = emp.Gender,
                Address = emp.Address,
                HireDate = emp.HireDate,
                Status = emp.Status,
                DepartmentId = emp.DepartmentId,
                DepartmentName = emp.Department?.DepartmentName,
                PositionId = emp.PositionId,
                PositionName = emp.Position?.PositionName
            };
        }

        public async Task<EmployeeDTO> Create(EmployeeDTO dto)
        {
            var emp = new Employee
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                HireDate = dto.HireDate,
                Status = dto.Status,
                DepartmentId = dto.DepartmentId,
                PositionId = dto.PositionId // 🔗 Lưu ID chức vụ
            };

            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();

            dto.Id = emp.Id;
            return dto;
        }

        public async Task<bool> Update(int id, EmployeeDTO dto)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;

            emp.FullName = dto.FullName;
            emp.Email = dto.Email;
            emp.Phone = dto.Phone;
            emp.DateOfBirth = dto.DateOfBirth;
            emp.Gender = dto.Gender;
            emp.Address = dto.Address;
            emp.HireDate = dto.HireDate;
            emp.Status = dto.Status;
            emp.DepartmentId = dto.DepartmentId;
            emp.PositionId = dto.PositionId; // 🔗 Cập nhật chức vụ mới

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}