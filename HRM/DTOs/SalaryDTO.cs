namespace HRM.DTOs
{
    public class SalaryDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? FullName { get; set; } // Để hiển thị lên Swagger/UI

        public int Month { get; set; }
        public int Year { get; set; }

        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Allowance { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}