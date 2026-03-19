namespace HRM.Models
{
    public class Department
    {
        public int Id { get; set; } // auto increment
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}