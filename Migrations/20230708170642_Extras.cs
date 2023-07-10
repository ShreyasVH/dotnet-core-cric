using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class Extras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    Runs = table.Column<int>(type: "int", nullable: false),
                    BattingTeamId = table.Column<long>(type: "bigint", nullable: false),
                    BowlingTeamId = table.Column<long>(type: "bigint", nullable: false),
                    Innings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_E_Batting_Team",
                        column: x => x.BattingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_E_Bowling_Team",
                        column: x => x.BowlingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_E_Match",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_E_Type",
                        column: x => x.TypeId,
                        principalTable: "ExtrasTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Batting_Team",
                table: "Extras",
                column: "BattingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_BowlingTeamId",
                table: "Extras",
                column: "BowlingTeamId");

            migrationBuilder.CreateIndex(
                name: "Match",
                table: "Extras",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "Type",
                table: "Extras",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "UK_E_Match_Type_Batting_Innings",
                table: "Extras",
                columns: new[] { "MatchId", "TypeId", "BattingTeamId", "Innings" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extras");
        }
    }
}
