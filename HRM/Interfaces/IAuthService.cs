using HRM.DTOs;
using HRM.Models;

namespace HRM.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);

        Task<List<UserDto>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, RegisterDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}