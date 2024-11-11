using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FORMULA1_Circuits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    WikipediaUrl = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Circuits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Constructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    WikipediaUrl = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Constructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    WikipediaUrl = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_GrandPrix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    WikipediaUrl = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_GrandPrix", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Seasons",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    WikipediaUrl = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Seasons", x => x.Year);
                });

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

            migrationBuilder.CreateTable(
                name: "FORMULA1_Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonYear = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    WikipediaUrl = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: true),
                    CircuitId = table.Column<int>(type: "int", nullable: true),
                    GrandPrixId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Races_FORMULA1_Circuits_CircuitId",
                        column: x => x.CircuitId,
                        principalTable: "FORMULA1_Circuits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FORMULA1_Races_FORMULA1_GrandPrix_GrandPrixId",
                        column: x => x.GrandPrixId,
                        principalTable: "FORMULA1_GrandPrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Races_FORMULA1_Seasons_SeasonYear",
                        column: x => x.SeasonYear,
                        principalTable: "FORMULA1_Seasons",
                        principalColumn: "Year",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionTypeId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Sessions_FORMULA1_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "FORMULA1_Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Sessions_FORMULA1_SessionTypes_SessionTypeId",
                        column: x => x.SessionTypeId,
                        principalTable: "FORMULA1_SessionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Ranking = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    ConstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Results_FORMULA1_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "FORMULA1_Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Results_FORMULA1_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "FORMULA1_Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Results_FORMULA1_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "FORMULA1_Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Races_CircuitId",
                table: "FORMULA1_Races",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Races_GrandPrixId",
                table: "FORMULA1_Races",
                column: "GrandPrixId");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Races_SeasonYear",
                table: "FORMULA1_Races",
                column: "SeasonYear");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Results_ConstructorId",
                table: "FORMULA1_Results",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Results_DriverId",
                table: "FORMULA1_Results",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Results_SessionId",
                table: "FORMULA1_Results",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Sessions_RaceId",
                table: "FORMULA1_Sessions",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FORMULA1_Sessions_SessionTypeId",
                table: "FORMULA1_Sessions",
                column: "SessionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FORMULA1_Results");

            migrationBuilder.DropTable(
                name: "FORMULA1_Constructors");

            migrationBuilder.DropTable(
                name: "FORMULA1_Drivers");

            migrationBuilder.DropTable(
                name: "FORMULA1_Sessions");

            migrationBuilder.DropTable(
                name: "FORMULA1_Races");

            migrationBuilder.DropTable(
                name: "FORMULA1_SessionTypes");

            migrationBuilder.DropTable(
                name: "FORMULA1_Circuits");

            migrationBuilder.DropTable(
                name: "FORMULA1_GrandPrix");

            migrationBuilder.DropTable(
                name: "FORMULA1_Seasons");
        }
    }
}
