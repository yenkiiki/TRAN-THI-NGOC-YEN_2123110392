using HRM.DTOs;

namespace HRM.Interfaces
{
    public interface IAttendanceService
    {
        Task<bool> CheckInAsync(int employeeId);
        Task<bool> CheckOutAsync(int employeeId);

        Task<List<AttendanceResponseDto>> GetAllAsync();
        Task<AttendanceResponseDto> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, AttendanceUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}