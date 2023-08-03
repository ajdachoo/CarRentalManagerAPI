using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalManagerAPI.Migrations
{
    public partial class Refactor4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PricePerDay",
                table: "Cars",
                type: "float",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PricePerDay",
                table: "Cars",
                type: "int",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 6);
        }
    }
}
