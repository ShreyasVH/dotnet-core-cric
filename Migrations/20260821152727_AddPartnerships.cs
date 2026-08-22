using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partnerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Innings = table.Column<int>(type: "int", nullable: false),
                    Wicket = table.Column<int>(type: "int", nullable: false),
                    Runs = table.Column<int>(type: "int", nullable: false),
                    Balls = table.Column<int>(type: "int", nullable: false),
                    Ended = table.Column<bool>(type: "bit", nullable: false),
                    MatchPlayerId1 = table.Column<int>(type: "int", nullable: false),
                    Runs1 = table.Column<int>(type: "int", nullable: false),
                    Balls1 = table.Column<int>(type: "int", nullable: false),
                    MatchPlayerId2 = table.Column<int>(type: "int", nullable: false),
                    Runs2 = table.Column<int>(type: "int", nullable: false),
                    Balls2 = table.Column<int>(type: "int", nullable: false),
                    PrimaryEntry = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BF_Match_Player_1",
                        column: x => x.MatchPlayerId1,
                        principalTable: "MatchPlayerMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BF_Match_Player_2",
                        column: x => x.MatchPlayerId2,
                        principalTable: "MatchPlayerMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UK_P_Players_Innings_Wicket",
                table: "Partnerships",
                columns: new[] { "MatchPlayerId1", "MatchPlayerId2", "Innings", "Wicket" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partnerships");
        }
    }
}
