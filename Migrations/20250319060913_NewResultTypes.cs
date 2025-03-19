using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class NewResultTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ResultTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Least Wickets" },
                    { "Boundary Count" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ResultTypes WHERE Name IN ('Least Wickets', 'Boundary Count')");
        }
    }
}
