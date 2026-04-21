namespace HRM.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Status { get; set; } = "Active";

        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        // 🔗 THÊM DỮ LIỆU POSITION
        public int PositionId { get; set; }
        public string? PositionName { get; set; }
    }
}