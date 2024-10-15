using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErgastCircuitId",
                table: "FORMULA1_Circuits");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "FORMULA1_Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "FORMULA1_Results");

            migrationBuilder.AddColumn<string>(
                name: "ErgastCircuitId",
                table: "FORMULA1_Circuits",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
