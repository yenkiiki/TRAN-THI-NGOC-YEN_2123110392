using HRM.DTOs;

namespace HRM.Interfaces
{
    public interface ISalaryService
    {
        Task<IEnumerable<SalaryDTO>> GetAllAsync();
        Task<SalaryDTO> GetByIdAsync(int id);
        Task<SalaryDTO> CreateAsync(SalaryDTO dto);
        Task<bool> UpdateAsync(int id, SalaryDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}