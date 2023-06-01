using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class TeamTypesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TeamTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "International" },
                    { "Domestic" },
                    { "Franchise" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamTypes",
                keyColumn: null,
                keyValues: null
            );
        }
    }
}
