using HRM.DTOs;

namespace HRM.Interfaces
{
    public interface IAttendanceService
    {
        // 🛠️ Sửa Task<bool> thành Task<string> để khớp với Service
        Task<string> CheckInAsync(int employeeId);
        Task<string> CheckOutAsync(int employeeId);

        Task<List<AttendanceResponseDto>> GetAllAsync();
        Task<AttendanceResponseDto> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, AttendanceUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}