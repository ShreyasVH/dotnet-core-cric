using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class MatchStaticData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ResultTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Normal" },
                    { "Tie" },
                    { "Draw" },
                    { "Super Over" },
                    { "Washed Out" },
                    { "Bowl Out" },
                    { "Forfeit" }
                }
            );
            
            migrationBuilder.InsertData(
                table: "WinMarginTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Run" },
                    { "Wicket" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM WinMarginTypes");

            migrationBuilder.Sql("DELETE FROM ResultTypes");
        }
    }
}