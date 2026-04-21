using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSalarySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Salaries");

            migrationBuilder.RenameColumn(
                name: "EffectiveDate",
                table: "Salaries",
                newName: "CalculationDate");

            migrationBuilder.RenameColumn(
                name: "BaseSalary",
                table: "Salaries",
                newName: "BaseSalaryAtTime");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "BaseSalary",
                table: "Positions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "BaseSalary",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "CalculationDate",
                table: "Salaries",
                newName: "EffectiveDate");

            migrationBuilder.RenameColumn(
                name: "BaseSalaryAtTime",
                table: "Salaries",
                newName: "BaseSalary");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Salaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Salaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Salaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
