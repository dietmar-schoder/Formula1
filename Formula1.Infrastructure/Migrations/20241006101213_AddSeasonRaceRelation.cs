using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasonRaceRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "FORMULA1_Races",
                newName: "SeasonYear");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Races_SeasonYear",
                table: "FORMULA1_Races",
                column: "SeasonYear");

            migrationBuilder.AddForeignKey(
                name: "FK_FORMULA1_Races_FORMULA1_Seasons_SeasonYear",
                table: "FORMULA1_Races",
                column: "SeasonYear",
                principalTable: "FORMULA1_Seasons",
                principalColumn: "Year",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FORMULA1_Races_FORMULA1_Seasons_SeasonYear",
                table: "FORMULA1_Races");

            migrationBuilder.DropIndex(
                name: "IX_FORMULA1_Races_SeasonYear",
                table: "FORMULA1_Races");

            migrationBuilder.RenameColumn(
                name: "SeasonYear",
                table: "FORMULA1_Races",
                newName: "Year");
        }
    }
}
