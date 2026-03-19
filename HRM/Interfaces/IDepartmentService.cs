using HRM.DTOs;

namespace HRM.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<DepartmentDto> CreateAsync(DepartmentDto dto);
        Task<bool> UpdateAsync(int id, DepartmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}