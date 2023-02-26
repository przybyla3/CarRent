using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRent.Migrations
{
    /// <inheritdoc />
    public partial class CarProductionYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductionYear",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionYear",
                table: "Cars");
        }
    }
}
