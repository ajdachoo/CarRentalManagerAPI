using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalManagerAPI.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrivingLicenseCategory",
                table: "Clients",
                newName: "drivingLicenseCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "drivingLicenseCategories",
                table: "Clients",
                newName: "DrivingLicenseCategory");
        }
    }
}
