using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewTableSessionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FORMULA1_SessionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_SessionTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FORMULA1_SessionTypes");
        }
    }
}
