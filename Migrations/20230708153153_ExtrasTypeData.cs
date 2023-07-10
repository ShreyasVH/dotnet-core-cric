using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class ExtrasTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExtrasTypes",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Bye" },
                    { "Leg Bye" },
                    { "Wide" },
                    { "No Ball" },
                    { "Penalty" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ExtrasTypes");
        }
    }
}
