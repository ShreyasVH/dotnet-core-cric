using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTagMapEntityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TagMap_EntityType_EntityId_TagId",
                table: "TagMap");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "TagMap");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Tags",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_EntityId_TagId",
                table: "TagMap",
                columns: new[] { "EntityId", "TagId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TagMap_EntityId_TagId",
                table: "TagMap");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Tags",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "EntityType",
                table: "TagMap",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_EntityType_EntityId_TagId",
                table: "TagMap",
                columns: new[] { "EntityType", "EntityId", "TagId" },
                unique: true);
        }
    }
}
