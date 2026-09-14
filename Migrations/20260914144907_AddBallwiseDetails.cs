using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddBallwiseDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BallwiseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatsmanMatchPlayerId = table.Column<int>(type: "int", nullable: false),
                    BowlerMatchPlayerId = table.Column<int>(type: "int", nullable: false),
                    Innings = table.Column<int>(type: "int", nullable: false),
                    Ball = table.Column<int>(type: "int", nullable: false),
                    TotalRuns = table.Column<int>(type: "int", nullable: false),
                    BatsmanRuns = table.Column<int>(type: "int", nullable: false),
                    BowlerRuns = table.Column<int>(type: "int", nullable: false),
                    ExtrasRuns = table.Column<int>(type: "int", nullable: false),
                    ExtrasType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dismissal = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BallwiseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BD_Batsman",
                        column: x => x.BatsmanMatchPlayerId,
                        principalTable: "MatchPlayerMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BD_Bowler",
                        column: x => x.BowlerMatchPlayerId,
                        principalTable: "MatchPlayerMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_BallwiseDetails_Batsman",
                table: "BallwiseDetails",
                column: "BatsmanMatchPlayerId");

            migrationBuilder.CreateIndex(
                name: "IDX_BallwiseDetails_Bowler",
                table: "BallwiseDetails",
                column: "BowlerMatchPlayerId");

            migrationBuilder.CreateIndex(
                name: "UK_BD_Timestamp",
                table: "BallwiseDetails",
                columns: new[] { "BowlerMatchPlayerId", "Timestamp" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BallwiseDetails");
        }
    }
}
