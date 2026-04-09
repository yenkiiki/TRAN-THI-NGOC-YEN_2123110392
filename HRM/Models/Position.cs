namespace HRM.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}