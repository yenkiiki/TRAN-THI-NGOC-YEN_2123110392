using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HRM.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Status { get; set; } = "Active";

        // 🔗 LIÊN KẾT DEPARTMENT (Đã có)
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [JsonIgnore]
        public virtual Department? Department { get; set; }

        // 🔗 LIÊN KẾT POSITION (MỚI THÊM)
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        [JsonIgnore]
        public virtual Position? Position { get; set; }

        [JsonIgnore]
        public virtual ICollection<Salary>? Salaries { get; set; }
        [JsonIgnore]
        public virtual ICollection<Attendance>? Attendances { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}