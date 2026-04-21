namespace HRM.DTOs
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}