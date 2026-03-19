using HRM.Data;
using HRM.DTOs;
using HRM.Models;
using HRM.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    DepartmentName = d.DepartmentName,
                    Description = d.Description,
                    IsActive = d.IsActive
                }).ToListAsync();
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var d = await _context.Departments.FindAsync(id);
            if (d == null) return null;

            return new DepartmentDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
                Description = d.Description,
                IsActive = d.IsActive
            };
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentDto dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            dto.Id = department.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, DepartmentDto dto)
        {
            var d = await _context.Departments.FindAsync(id);
            if (d == null) return false;

            d.DepartmentName = dto.DepartmentName;
            d.Description = dto.Description;
            d.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var d = await _context.Departments.FindAsync(id);
            if (d == null) return false;

            _context.Departments.Remove(d);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}