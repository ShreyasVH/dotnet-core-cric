using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class SeriesTypesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SeriesTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Bilateral" },
                    { "Tri series" },
                    { "Tournament" }
                }
            );
            
            migrationBuilder.InsertData(
                table: "GameTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "ODI" },
                    { "Test" },
                    { "T20" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SeriesTypes",
                keyColumn: null,
                keyValues: null
            );
            
            migrationBuilder.DeleteData(
                table: "GameTypes",
                keyColumn: null,
                keyValues: null
            );
        }
    }
}
