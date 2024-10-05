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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    ErgastCircuitId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Circuits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Constructors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Constructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Races",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    CircuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMULA1_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FORMULA1_Races_FORMULA1_Circuits_CircuitId",
                        column: x => x.CircuitId,
                        principalTable: "FORMULA1_Circuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionTypeId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "FORMULA1_Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "FORMULA1_Circuits");
        }
    }
}
