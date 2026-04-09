using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.Migrations
{
    /// <inheritdoc />
    public partial class FixSalaryEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TongLuong",
                table: "Salaries",
                newName: "TotalSalary");

            migrationBuilder.RenameColumn(
                name: "Thuong",
                table: "Salaries",
                newName: "Bonus");

            migrationBuilder.RenameColumn(
                name: "PhuCap",
                table: "Salaries",
                newName: "BaseSalary");

            migrationBuilder.RenameColumn(
                name: "NgayApDung",
                table: "Salaries",
                newName: "EffectiveDate");

            migrationBuilder.RenameColumn(
                name: "LuongCoBan",
                table: "Salaries",
                newName: "Allowance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSalary",
                table: "Salaries",
                newName: "TongLuong");

            migrationBuilder.RenameColumn(
                name: "EffectiveDate",
                table: "Salaries",
                newName: "NgayApDung");

            migrationBuilder.RenameColumn(
                name: "Bonus",
                table: "Salaries",
                newName: "Thuong");

            migrationBuilder.RenameColumn(
                name: "BaseSalary",
                table: "Salaries",
                newName: "PhuCap");

            migrationBuilder.RenameColumn(
                name: "Allowance",
                table: "Salaries",
                newName: "LuongCoBan");
        }
    }
}
