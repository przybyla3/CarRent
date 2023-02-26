using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRent.Migrations
{
    /// <inheritdoc />
    public partial class AddCostToRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Rentals",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Rentals");
        }
    }
}
