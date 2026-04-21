namespace HRM.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Liên kết ngược lại (1 phòng nhiều NV)
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}