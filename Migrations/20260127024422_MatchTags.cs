using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class MatchTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "FINAL" },
                    { "SEMI_FINAL" },
                    { "QUARTER_FINAL" },
                    { "KNOCKOUT" },
                    { "ELIMINATOR" },
                    { "THIRD_PLACE" },
                    { "QUALIFIER_1" },
                    { "QUALIFIER_2" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Tags WHERE Name IN ('FINAL', 'SEMI_FINAL', 'QUARTER_FINAL', 'KNOCKOUT', 'ELIMINATOR', 'THIRD_PLACE', 'QUALIFIER_1', 'QUALIFIER_2')");
        }
    }
}
