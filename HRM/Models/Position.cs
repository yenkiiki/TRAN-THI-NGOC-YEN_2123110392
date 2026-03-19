namespace HRM.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string TenChucVu { get; set; } 
        public string MoTa { get; set; } 
        public bool HoatDong { get; set; } = true; 
    }
}