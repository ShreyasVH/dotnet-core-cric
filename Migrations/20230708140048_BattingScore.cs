using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class BattingScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BattingScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchPlayerId = table.Column<int>(type: "int", nullable: false),
                    Runs = table.Column<int>(type: "int", nullable: false),
                    Balls = table.Column<int>(type: "int", nullable: false),
                    Fours = table.Column<int>(type: "int", nullable: false),
                    Sixes = table.Column<int>(type: "int", nullable: false),
                    DismissalModeId = table.Column<byte>(type: "tinyint", nullable: true),
                    BowlerId = table.Column<int>(type: "int", nullable: true),
                    Innings = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattingScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BS_Bowler",
                        column: x => x.BowlerId,
                        principalTable: "MatchPlayerMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BS_Dismissal_Mode",
                        column: x => x.DismissalModeId,
                        principalTable: "DismissalModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BS_Match_Player",
                        column: x => x.MatchPlayerId,
                        principalTable: "MatchPlayerMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Bowler",
                table: "BattingScores",
                column: "BowlerId");

            migrationBuilder.CreateIndex(
                name: "Dismissal_Mode",
                table: "BattingScores",
                column: "DismissalModeId");

            migrationBuilder.CreateIndex(
                name: "Match_Player",
                table: "BattingScores",
                column: "MatchPlayerId");

            migrationBuilder.CreateIndex(
                name: "UK_BS_Match_Player_Innings",
                table: "BattingScores",
                columns: new[] { "MatchPlayerId", "Innings" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattingScores");
        }
    }
}
