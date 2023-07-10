using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class DismissalModeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DismissalModes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Bowled" },
                    { "Caught" },
                    { "LBW" },
                    { "Run Out" },
                    { "Stumped" },
                    { "Hit Twice" },
                    { "Hit Wicket" },
                    { "Obstructing the Field" },
                    { "Timed Out" },
                    { "Retired Hurt" },
                    { "Handled the Ball" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM DismissalModes");
        }
    }
}