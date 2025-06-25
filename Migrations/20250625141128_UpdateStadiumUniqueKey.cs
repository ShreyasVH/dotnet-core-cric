using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStadiumUniqueKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stadiums_Name_CountryId",
                table: "Stadiums"
            );
            
            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_Name_CountryId_City",
                table: "Stadiums",
                columns: new[] { "Name", "CountryId", "City" },
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stadiums_Name_CountryId_City",
                table: "Stadiums"
            );
            
            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_Name_CountryId",
                table: "Stadiums",
                columns: new[] { "Name", "CountryId" },
                unique: true
            );
        }
    }
}