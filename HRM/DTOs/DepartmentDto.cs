using System.ComponentModel.DataAnnotations;

namespace HRM.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        [StringLength(100, ErrorMessage = "Tên phòng ban không quá 100 ký tự")]
        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}