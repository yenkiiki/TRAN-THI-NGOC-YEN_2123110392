using Microsoft.EntityFrameworkCore;
using HRM.Models;

namespace HRM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        // Config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================= SALARY =================
            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(e => e.BaseSalary).HasPrecision(18, 2);
                entity.Property(e => e.Bonus).HasPrecision(18, 2);
                entity.Property(e => e.Allowance).HasPrecision(18, 2);
                entity.Property(e => e.TotalSalary).HasPrecision(18, 2);

                entity.HasOne(e => e.Employee)
                      .WithMany(e => e.Salaries)
                      .HasForeignKey(e => e.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ================= USER - EMPLOYEE (1-1) =================
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<User>(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            // ================= EMPLOYEE - ATTENDANCE (1-N) =================
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // ================= UNIQUE: 1 employee / 1 day =================
            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.EmployeeId, a.Date })
                .IsUnique();

            // ================= PRECISION (nếu có WorkHours dạng decimal/double lưu DB) =================
            modelBuilder.Entity<Attendance>()
                .Property(a => a.WorkHours)
                .HasPrecision(5, 2);
        }
    }
}