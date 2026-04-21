using Microsoft.EntityFrameworkCore;
using HRM.Models;

namespace HRM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================= USER CONFIG (NÂNG CẤP) =================
            modelBuilder.Entity<User>(entity =>
            {
                // Validate duy nhất Username ở tầng DB
                entity.HasIndex(u => u.Username).IsUnique();

                // Ràng buộc độ dài (khớp với DTO)
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Role).IsRequired().HasMaxLength(20);

                // Mối quan hệ 1-1 với Employee
                entity.HasOne(u => u.Employee)
                    .WithOne(e => e.User)
                    .HasForeignKey<User>(u => u.EmployeeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ================= EMPLOYEE - DEPT - POSITION =================
            // Đảm bảo các liên kết cơ bản được định nghĩa rõ ràng
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict); // Không xóa phòng ban nếu còn nhân viên

                entity.HasOne(e => e.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(e => e.PositionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ================= SALARY (GIỮ NGUYÊN & TỐI ƯU) =================
            // Tìm đến đoạn SALARY (GIỮ NGUYÊN & TỐI ƯU) và sửa lại như sau:
            modelBuilder.Entity<Salary>(entity =>
            {
                // Sửa BaseSalary thành BaseSalaryAtTime để khớp với Model Salary.cs
                entity.Property(e => e.BaseSalaryAtTime).HasPrecision(18, 2);
                entity.Property(e => e.Bonus).HasPrecision(18, 2);
                entity.Property(e => e.Allowance).HasPrecision(18, 2);
                entity.Property(e => e.TotalSalary).HasPrecision(18, 2);

                entity.HasOne(e => e.Employee)
                      .WithMany(e => e.Salaries)
                      .HasForeignKey(e => e.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ================= ATTENDANCE (GIỮ NGUYÊN & TỐI ƯU) =================
            modelBuilder.Entity<Attendance>(entity =>
            {
                // Đảm bảo một nhân viên chỉ có 1 bản ghi chấm công mỗi ngày
                entity.HasIndex(a => new { a.EmployeeId, a.Date }).IsUnique();

                entity.Property(a => a.WorkHours).HasPrecision(5, 2);

                entity.HasOne(a => a.Employee)
                    .WithMany(e => e.Attendances)
                    .HasForeignKey(a => a.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}