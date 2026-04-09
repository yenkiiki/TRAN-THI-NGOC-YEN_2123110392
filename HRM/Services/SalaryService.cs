using HRM.Data;
using HRM.DTOs;
using HRM.Interfaces;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly AppDbContext _context;

        public SalaryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaryDTO>> GetAllAsync()
        {
            return await _context.Salaries
                .Select(s => new SalaryDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    FullName = s.FullName,
                    Email = s.Email,
                    Phone = s.Phone,
                    BaseSalary = s.BaseSalary,
                    Bonus = s.Bonus,
                    Allowance = s.Allowance,
                    TotalSalary = s.TotalSalary,
                    EffectiveDate = s.EffectiveDate
                }).ToListAsync();
        }

        public async Task<SalaryDTO> GetByIdAsync(int id)
        {
            var s = await _context.Salaries.FindAsync(id);
            if (s == null) return null;

            return new SalaryDTO
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                BaseSalary = s.BaseSalary,
                Bonus = s.Bonus,
                Allowance = s.Allowance,
                TotalSalary = s.TotalSalary,
                EffectiveDate = s.EffectiveDate
            };
        }

        public async Task<SalaryDTO> CreateAsync(SalaryDTO dto)
        {
            var emp = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == dto.EmployeeId);

            if (emp == null)
                throw new Exception("Employee không tồn tại!");

            var salary = new Salary
            {
                EmployeeId = dto.EmployeeId,

                // copy info
                FullName = emp.FullName,
                Email = emp.Email,
                Phone = emp.Phone,

                BaseSalary = dto.BaseSalary,
                Bonus = dto.Bonus,
                Allowance = dto.Allowance,
                TotalSalary = dto.BaseSalary + dto.Bonus + dto.Allowance,
                EffectiveDate = dto.EffectiveDate
            };

            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();

            return new SalaryDTO
            {
                Id = salary.Id,
                EmployeeId = salary.EmployeeId,
                FullName = salary.FullName,
                Email = salary.Email,
                Phone = salary.Phone,
                BaseSalary = salary.BaseSalary,
                Bonus = salary.Bonus,
                Allowance = salary.Allowance,
                TotalSalary = salary.TotalSalary,
                EffectiveDate = salary.EffectiveDate
            };
        }

        public async Task<bool> UpdateAsync(int id, SalaryDTO dto)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return false;

            salary.EmployeeId = dto.EmployeeId;
            salary.BaseSalary = dto.BaseSalary;
            salary.Bonus = dto.Bonus;
            salary.Allowance = dto.Allowance;
            salary.TotalSalary = dto.BaseSalary + dto.Bonus + dto.Allowance;
            salary.EffectiveDate = dto.EffectiveDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return false;

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}