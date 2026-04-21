using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        public decimal BaseSalaryAtTime { get; set; } // Lưu vết lương lúc tính
        public decimal Bonus { get; set; }
        public decimal Allowance { get; set; }
        public decimal TotalSalary { get; set; }

        public DateTime CalculationDate { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
    }
}