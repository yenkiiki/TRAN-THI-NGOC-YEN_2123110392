using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.Models
{
    public class Salary
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        // Employee info
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Salary info
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Allowance { get; set; }
        public decimal TotalSalary { get; set; }

        public DateTime EffectiveDate { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}