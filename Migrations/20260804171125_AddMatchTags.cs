using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "FINAL",
                    "SEMI_FINAL",
                    "QUARTER_FINAL",
                    "KNOCKOUT",
                    "ELIMINATOR",
                    "THIRD_PLACE",
                    "QUALIFIER",
                    "QUALIFIER_1",
                    "QUALIFIER_2",
                    "CHALLENGER"
                },
                column: "Type",
                values: new object[]
                {
                    "MATCH",
                    "MATCH",
                    "MATCH",
                    "MATCH",
                    "MATCH",
                    "MATCH",
                    "MATCH",
                    "MATCH",
                    "MATCH",
                    "MATCH"
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "FINAL",
                    "SEMI_FINAL",
                    "QUARTER_FINAL",
                    "KNOCKOUT",
                    "ELIMINATOR",
                    "THIRD_PLACE",
                    "QUALIFIER",
                    "QUALIFIER_1",
                    "QUALIFIER_2",
                    "CHALLENGER"
                },
                column: "Type",
                values: new object[]
                {
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                }
            );
        }
    }
}
