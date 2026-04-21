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

        public async Task<string> CheckInAsync(int employeeId)
        {
            var now = DateTime.Now;
            var today = now.Date;

            // 1. Kiểm tra nhân viên
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return "Nhân viên không tồn tại!";
            if (employee.Status != "Active") return "Nhân viên này không còn hoạt động!";

            // 2. Kiểm tra xem đã check-in chưa
            var exists = await _context.Attendances
                .AnyAsync(x => x.EmployeeId == employeeId && x.Date == today);
            if (exists) return "Bạn đã Check-in hôm nay rồi!";

            // 3. Logic trạng thái
            var lateThreshold = new TimeSpan(8, 30, 0);
            var status = now.TimeOfDay <= lateThreshold ? "Present" : "Late";

            var newAttendance = new Attendance
            {
                EmployeeId = employeeId,
                Date = today,
                CheckIn = now,
                Status = status,
                CreatedAt = now
            };

            _context.Attendances.Add(newAttendance);
            await _context.SaveChangesAsync();
            return "Check-in thành công!";
        }

        public async Task<string> CheckOutAsync(int employeeId)
        {
            var now = DateTime.Now;
            var today = now.Date;

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Date == today);

            if (attendance == null) return "Bạn chưa Check-in sáng nay!";
            if (attendance.CheckOut != null) return "Bạn đã Check-out rồi!";

            attendance.CheckOut = now;
            attendance.UpdatedAt = now;

            if (attendance.CheckIn.HasValue)
            {
                var duration = now - attendance.CheckIn.Value;
                double totalHours = duration.TotalHours;
                if (totalHours > 5) totalHours -= 1; // Nghỉ trưa
                attendance.WorkHours = Math.Round(totalHours, 2);
            }

            await _context.SaveChangesAsync();
            return "Check-out thành công!";
        }

        // --- Các hàm bên dưới giữ nguyên kiểu trả về vì nó đã khớp ---

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