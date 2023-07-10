using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class FielderDismissal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FielderDismissals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoreId = table.Column<int>(type: "int", nullable: false),
                    MatchPlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FielderDismissals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FD_Match_Player",
                        column: x => x.MatchPlayerId,
                        principalTable: "MatchPlayerMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FD_Score",
                        column: x => x.ScoreId,
                        principalTable: "BattingScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Match_Player",
                table: "FielderDismissals",
                column: "MatchPlayerId");

            migrationBuilder.CreateIndex(
                name: "Score",
                table: "FielderDismissals",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "UK_FD_Score_Player_Team",
                table: "FielderDismissals",
                columns: new[] { "ScoreId", "MatchPlayerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FielderDismissals");
        }
    }
}
