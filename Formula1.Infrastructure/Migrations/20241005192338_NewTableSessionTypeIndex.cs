using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewTableSessionTypeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Sessions_SessionTypeId",
                table: "FORMULA1_Sessions",
                column: "SessionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FORMULA1_Sessions_FORMULA1_SessionTypes_SessionTypeId",
                table: "FORMULA1_Sessions",
                column: "SessionTypeId",
                principalTable: "FORMULA1_SessionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FORMULA1_Sessions_FORMULA1_SessionTypes_SessionTypeId",
                table: "FORMULA1_Sessions");

            migrationBuilder.DropIndex(
                name: "IX_FORMULA1_Sessions_SessionTypeId",
                table: "FORMULA1_Sessions");
        }
    }
}
