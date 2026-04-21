using System.ComponentModel.DataAnnotations;

namespace HRM.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username không được để trống")]
        [MinLength(4, ErrorMessage = "Username phải ít nhất 4 ký tự")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password không được để trống")]
        [MinLength(6, ErrorMessage = "Password phải từ 6 ký tự trở lên")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Admin|HR|Employee)$", ErrorMessage = "Role phải là Admin, HR hoặc Employee")]
        public string Role { get; set; } = "Employee";

        // Id nhân viên để gắn tài khoản vào
        public int? EmployeeId { get; set; }
    }
}