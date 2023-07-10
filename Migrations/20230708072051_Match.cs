using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class Match : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WinMarginTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinMarginTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesId = table.Column<long>(type: "bigint", nullable: false),
                    Team1Id = table.Column<long>(type: "bigint", nullable: false),
                    Team2Id = table.Column<long>(type: "bigint", nullable: false),
                    TossWinnerId = table.Column<long>(type: "bigint", nullable: true),
                    BatFirstId = table.Column<long>(type: "bigint", nullable: true),
                    ResultTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    WinnerId = table.Column<long>(type: "bigint", nullable: true),
                    WinMargin = table.Column<int>(type: "int", nullable: true),
                    WinMarginTypeId = table.Column<byte>(type: "tinyint", nullable: true),
                    StadiumId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOfficial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_ResultTypes_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_BatFirstId",
                        column: x => x.BatFirstId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TossWinnerId",
                        column: x => x.TossWinnerId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_WinMarginTypes_WinMarginTypeId",
                        column: x => x.WinMarginTypeId,
                        principalTable: "WinMarginTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "BatFirst",
                table: "Matches",
                column: "BatFirstId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StadiumId_StartTime",
                table: "Matches",
                columns: new[] { "StadiumId", "StartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ResultType",
                table: "Matches",
                column: "ResultTypeId");

            migrationBuilder.CreateIndex(
                name: "Series",
                table: "Matches",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "Stadium",
                table: "Matches",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "Team1",
                table: "Matches",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "Team2",
                table: "Matches",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "TossWinner",
                table: "Matches",
                column: "TossWinnerId");

            migrationBuilder.CreateIndex(
                name: "WinMarginType",
                table: "Matches",
                column: "WinMarginTypeId");

            migrationBuilder.CreateIndex(
                name: "Winner",
                table: "Matches",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultTypes_Name",
                table: "ResultTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WinMarginTypes_Name",
                table: "WinMarginTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "ResultTypes");

            migrationBuilder.DropTable(
                name: "WinMarginTypes");
        }
    }
}
