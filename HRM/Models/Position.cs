namespace HRM.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; } // Lương cơ bản của chức vụ
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Employee>? Employees { get; set; }
    }
}