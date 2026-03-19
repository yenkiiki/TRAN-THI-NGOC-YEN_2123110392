using HRM.DTOs;

namespace HRM.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAll();
        Task<EmployeeDTO?> GetById(int id);
        Task<EmployeeDTO> Create(EmployeeDTO dto);
        Task<bool> Update(int id, EmployeeDTO dto);
        Task<bool> Delete(int id);
    }
}