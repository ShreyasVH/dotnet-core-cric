using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AddSeriesTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Tags",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
            
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "WORLD_CUP",
                    "IPL",
                    "CHAMPIONS_TROPHY",
                    "BBL",
                    "ILT20",
                    "CHAMPIONS_LEAGUE",
                    "ASIA_CUP",
                    "WTC",
                    "CPL"
                },
                column: "Type",
                values: new object[]
                {
                    "SERIES",
                    "SERIES",
                    "SERIES",
                    "SERIES",
                    "SERIES",
                    "SERIES",
                    "SERIES",
                    "SERIES",
                    "SERIES"
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tags");
        }
    }
}
