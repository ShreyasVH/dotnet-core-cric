using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class Totals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Totals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: false),
                    Runs = table.Column<int>(type: "int", nullable: false),
                    Wickets = table.Column<int>(type: "int", nullable: false),
                    Balls = table.Column<int>(type: "int", nullable: false),
                    Innings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Totals", x => x.Id);
                    table.ForeignKey(
                        name: "fk_t_match",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_t_team",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Totals_TeamId",
                table: "Totals",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "Match",
                table: "Totals",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "UK_T_Match_Team_Innings",
                table: "Totals",
                columns: new[] { "MatchId", "TeamId", "Innings" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Totals");
        }
    }
}
