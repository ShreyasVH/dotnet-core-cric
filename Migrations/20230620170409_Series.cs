using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class Series : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeriesTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HomeCountryId = table.Column<long>(type: "bigint", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    GameTypeId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Countries_HomeCountryId",
                        column: x => x.HomeCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Series_GameTypes_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "GameTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Series_SeriesTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SeriesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Series_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeriesTeamsMap",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesId = table.Column<long>(type: "bigint", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesTeamsMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesTeamsMap_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeriesTeamsMap_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameTypes_Name",
                table: "GameTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "GameType",
                table: "Series",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "HomeCountry",
                table: "Series",
                column: "HomeCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Name_TourId_GameTypeId",
                table: "Series",
                columns: new[] { "Name", "TourId", "GameTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Tour",
                table: "Series",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "Type",
                table: "Series",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesTeamsMap_SeriesId_TeamId",
                table: "SeriesTeamsMap",
                columns: new[] { "SeriesId", "TeamId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Series",
                table: "SeriesTeamsMap",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "Team",
                table: "SeriesTeamsMap",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesTypes_Name",
                table: "SeriesTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeriesTeamsMap");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "GameTypes");

            migrationBuilder.DropTable(
                name: "SeriesTypes");
        }
    }
}
