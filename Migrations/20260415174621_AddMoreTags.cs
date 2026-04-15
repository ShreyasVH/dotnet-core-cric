using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "CPL" },
                    { "CHALLENGER" },
                    { "QUALIFIER" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Tags WHERE Name IN ('CPL', 'CHALLENGER', 'QUALIFIER')");
        }
    }
}
