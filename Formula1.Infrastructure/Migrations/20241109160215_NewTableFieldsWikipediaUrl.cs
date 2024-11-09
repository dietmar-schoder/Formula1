using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewTableFieldsWikipediaUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WikipediaUrl",
                table: "FORMULA1_Seasons",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1023)",
                oldMaxLength: 1023);

            migrationBuilder.AddColumn<string>(
                name: "Ranking",
                table: "FORMULA1_Results",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WikipediaUrl",
                table: "FORMULA1_Races",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WikipediaUrl",
                table: "FORMULA1_GrandPrix",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WikipediaUrl",
                table: "FORMULA1_Drivers",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WikipediaUrl",
                table: "FORMULA1_Constructors",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WikipediaUrl",
                table: "FORMULA1_Circuits",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ranking",
                table: "FORMULA1_Results");

            migrationBuilder.DropColumn(
                name: "WikipediaUrl",
                table: "FORMULA1_Races");

            migrationBuilder.DropColumn(
                name: "WikipediaUrl",
                table: "FORMULA1_GrandPrix");

            migrationBuilder.DropColumn(
                name: "WikipediaUrl",
                table: "FORMULA1_Drivers");

            migrationBuilder.DropColumn(
                name: "WikipediaUrl",
                table: "FORMULA1_Constructors");

            migrationBuilder.DropColumn(
                name: "WikipediaUrl",
                table: "FORMULA1_Circuits");

            migrationBuilder.AlterColumn<string>(
                name: "WikipediaUrl",
                table: "FORMULA1_Seasons",
                type: "nvarchar(1023)",
                maxLength: 1023,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1023)",
                oldMaxLength: 1023,
                oldNullable: true);
        }
    }
}
