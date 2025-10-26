using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class SeriesTypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeriesTeamsMap_Series_SeriesId",
                table: "SeriesTeamsMap"
            );
            
            migrationBuilder.DropForeignKey(
                name: "FK_ManOfTheSeries_Series_SeriesId",
                table: "ManOfTheSeries"
            );
            
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Series_SeriesId",
                table: "Matches"
            );
            
            migrationBuilder.AlterColumn<int>(
                name: "SeriesId",
                table: "SeriesTeamsMap",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series"
            );
            
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Series",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Id"
            );

            migrationBuilder.AlterColumn<int>(
                name: "SeriesId",
                table: "Matches",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "SeriesId",
                table: "ManOfTheSeries",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
            
            migrationBuilder.AddForeignKey(
                name: "FK_SeriesTeamsMap_Series_SeriesId",
                table: "SeriesTeamsMap",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id"
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_ManOfTheSeries_Series_SeriesId",
                table: "ManOfTheSeries",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id"
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Series_SeriesId",
                table: "Matches",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id"
            );

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });
            
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "WORLD_CUP" },
                    { "IPL" },
                    { "CHAMPIONS_TROPHY" },
                    { "BBL" },
                    { "ILT20" },
                    { "CHAMPIONS_LEAGUE" },
                    { "ASIA_CUP" },
                    { "WTC" }
                }
            );

            migrationBuilder.CreateTable(
                name: "TagMap",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagMap_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_EntityType_EntityId_TagId",
                table: "TagMap",
                columns: new[] { "EntityType", "EntityId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Tag",
                table: "TagMap",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagMap");

            migrationBuilder.DropTable(
                name: "Tags");
            
            migrationBuilder.DropForeignKey(
                name: "FK_SeriesTeamsMap_Series_SeriesId",
                table: "SeriesTeamsMap"
            );
            
            migrationBuilder.DropForeignKey(
                name: "FK_ManOfTheSeries_Series_SeriesId",
                table: "ManOfTheSeries"
            );
            
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Series_SeriesId",
                table: "Matches"
            );

            migrationBuilder.AlterColumn<long>(
                name: "SeriesId",
                table: "SeriesTeamsMap",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series"
            );
            
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Series",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Id"
            );

            migrationBuilder.AlterColumn<long>(
                name: "SeriesId",
                table: "Matches",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "SeriesId",
                table: "ManOfTheSeries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
            
            migrationBuilder.AddForeignKey(
                name: "FK_SeriesTeamsMap_Series_SeriesId",
                table: "SeriesTeamsMap",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id"
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_ManOfTheSeries_Series_SeriesId",
                table: "ManOfTheSeries",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id"
            );
            
            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Series_SeriesId",
                table: "Matches",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id"
            );
        }
    }
}
