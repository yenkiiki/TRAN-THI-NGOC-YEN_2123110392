using HRM.DTOs;

namespace HRM.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionDto>> GetAllAsync();           // Lấy tất cả chức vụ
        Task<PositionDto?> GetByIdAsync(int id);               // Lấy 1 chức vụ theo Id
        Task<PositionDto> CreateAsync(PositionDto dto);        // Tạo mới chức vụ
        Task<bool> UpdateAsync(int id, PositionDto dto);       // Cập nhật chức vụ
        Task<bool> DeleteAsync(int id);                        // Xóa chức vụ
    }
}