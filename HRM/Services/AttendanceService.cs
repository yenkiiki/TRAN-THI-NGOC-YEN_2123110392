using HRM.Data;
using HRM.DTOs;
using HRM.Interfaces;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AppDbContext _context;

        public AttendanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckInAsync(int employeeId)
        {
            var now = DateTime.Now; // 🕒 Lấy giờ thực tế máy tính
            var today = now.Date;

            // Kiểm tra xem nhân viên có tồn tại không
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId);
            if (!employeeExists) return false;

            // Kiểm tra xem hôm nay đã check-in chưa
            var exists = await _context.Attendances
                .AnyAsync(x => x.EmployeeId == employeeId && x.Date == today);
            if (exists) return false;

            var newAttendance = new Attendance
            {
                EmployeeId = employeeId,
                Date = today,
                CheckIn = now,
                Status = now.Hour < 8 ? "Present" : "Late", // Trước 8h là Present, sau 8h là Trễ
                CreatedAt = now
            };

            _context.Attendances.Add(newAttendance);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckOutAsync(int employeeId)
        {
            var now = DateTime.Now;
            var today = now.Date;

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Date == today);

            if (attendance == null || attendance.CheckOut != null) return false;

            attendance.CheckOut = now;
            attendance.UpdatedAt = now;

            if (attendance.CheckIn != null)
            {
                // Tính số giờ làm việc (định dạng số thập phân, ví dụ: 8.5 giờ)
                attendance.WorkHours = Math.Round((attendance.CheckOut.Value - attendance.CheckIn.Value).TotalHours, 2);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AttendanceResponseDto>> GetAllAsync()
        {
            return await _context.Attendances
                .Include(x => x.Employee)
                .Select(x => new AttendanceResponseDto
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FullName,
                    Date = x.Date,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    WorkHours = x.WorkHours,
                    Status = x.Status
                }).ToListAsync();
        }

        public async Task<AttendanceResponseDto> GetByIdAsync(int id)
        {
            var x = await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (x == null) return null;

            return new AttendanceResponseDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee.FullName,
                Date = x.Date,
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut,
                WorkHours = x.WorkHours,
                Status = x.Status
            };
        }

        public async Task<bool> UpdateAsync(int id, AttendanceUpdateDto dto)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return false;

            if (dto.CheckIn != null) attendance.CheckIn = dto.CheckIn;
            if (dto.CheckOut != null) attendance.CheckOut = dto.CheckOut;
            if (!string.IsNullOrEmpty(dto.Status)) attendance.Status = dto.Status;

            if (attendance.CheckIn != null && attendance.CheckOut != null)
            {
                attendance.WorkHours = Math.Round((attendance.CheckOut.Value - attendance.CheckIn.Value).TotalHours, 2);
            }

            attendance.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return false;

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}