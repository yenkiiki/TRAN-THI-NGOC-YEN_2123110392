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
        public SalaryService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<SalaryDTO>> GetAllAsync()
        {
            return await _context.Salaries
                .Include(s => s.Employee)
                .Select(s => new SalaryDTO
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    FullName = s.Employee != null ? s.Employee.FullName : "N/A",
                    Month = s.Month,
                    Year = s.Year,
                    BaseSalary = s.BaseSalaryAtTime,
                    Bonus = s.Bonus,
                    Allowance = s.Allowance,
                    TotalSalary = s.TotalSalary,
                    EffectiveDate = s.CalculationDate
                }).ToListAsync();
        }

        // --- FIX LỖI THIẾU GetByIdAsync ---
        public async Task<SalaryDTO?> GetByIdAsync(int id)
        {
            var s = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (s == null) return null;

            return new SalaryDTO
            {
                Id = s.Id,
                EmployeeId = s.EmployeeId,
                FullName = s.Employee?.FullName,
                Month = s.Month,
                Year = s.Year,
                BaseSalary = s.BaseSalaryAtTime,
                Bonus = s.Bonus,
                Allowance = s.Allowance,
                TotalSalary = s.TotalSalary,
                EffectiveDate = s.CalculationDate
            };
        }

        public async Task<SalaryDTO> CreateAsync(SalaryDTO dto)
        {
            var emp = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == dto.EmployeeId);

            if (emp == null) throw new Exception("Nhân viên không tồn tại!");
            if (emp.Position == null) throw new Exception("Nhân viên chưa có chức vụ!");

            var salary = new Salary
            {
                EmployeeId = dto.EmployeeId,
                Month = dto.Month == 0 ? DateTime.Now.Month : dto.Month,
                Year = dto.Year == 0 ? DateTime.Now.Year : dto.Year,
                BaseSalaryAtTime = emp.Position.BaseSalary,
                Bonus = dto.Bonus,
                Allowance = dto.Allowance,
                TotalSalary = emp.Position.BaseSalary + dto.Bonus + dto.Allowance,
                CalculationDate = DateTime.Now
            };

            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();

            dto.Id = salary.Id;
            dto.BaseSalary = salary.BaseSalaryAtTime;
            dto.TotalSalary = salary.TotalSalary;
            dto.FullName = emp.FullName;
            dto.EffectiveDate = salary.CalculationDate;
            return dto;
        }

        // --- FIX LỖI THIẾU UpdateAsync ---
        public async Task<bool> UpdateAsync(int id, SalaryDTO dto)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return false;

            salary.Month = dto.Month;
            salary.Year = dto.Year;
            salary.Bonus = dto.Bonus;
            salary.Allowance = dto.Allowance;
            // Tính lại tổng dựa trên lương cũ đã lưu vết
            salary.TotalSalary = salary.BaseSalaryAtTime + dto.Bonus + dto.Allowance;

            _context.Entry(salary).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var s = await _context.Salaries.FindAsync(id);
            if (s == null) return false;
            _context.Salaries.Remove(s);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}