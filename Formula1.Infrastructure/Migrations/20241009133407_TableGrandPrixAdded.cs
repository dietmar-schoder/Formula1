using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableGrandPrixAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GrandPrixId",
                table: "FORMULA1_Races",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FORMULA1_GrandPrix",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_GrandPrix", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Races_GrandPrixId",
                table: "FORMULA1_Races",
                column: "GrandPrixId");

            migrationBuilder.AddForeignKey(
                name: "FK_FORMULA1_Races_FORMULA1_GrandPrix_GrandPrixId",
                table: "FORMULA1_Races",
                column: "GrandPrixId",
                principalTable: "FORMULA1_GrandPrix",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FORMULA1_Races_FORMULA1_GrandPrix_GrandPrixId",
                table: "FORMULA1_Races");

            migrationBuilder.DropTable(
                name: "FORMULA1_GrandPrix");

            migrationBuilder.DropIndex(
                name: "IX_FORMULA1_Races_GrandPrixId",
                table: "FORMULA1_Races");

            migrationBuilder.DropColumn(
                name: "GrandPrixId",
                table: "FORMULA1_Races");
        }
    }
}
