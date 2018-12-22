using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataViz.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueryCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Columns = table.Column<List<string>>(nullable: true),
                    Conditionals = table.Column<Dictionary<string, string>>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueryPopover",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Toggle = table.Column<bool>(nullable: false),
                    Additional = table.Column<List<string>>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryPopover", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueryRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TableName = table.Column<string>(nullable: true),
                    XCol = table.Column<string>(nullable: true),
                    YCols = table.Column<List<string>>(nullable: true),
                    GraphType = table.Column<string>(nullable: true),
                    GraphSubType = table.Column<string>(nullable: true),
                    Filters = table.Column<string>(nullable: true),
                    CategoriesId = table.Column<int>(nullable: true),
                    Functions = table.Column<Dictionary<string, string>>(nullable: true),
                    PopoverId = table.Column<int>(nullable: true),
                    Appearance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueryRequests_QueryCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "QueryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QueryRequests_QueryPopover_PopoverId",
                        column: x => x.PopoverId,
                        principalTable: "QueryPopover",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueryRequests_CategoriesId",
                table: "QueryRequests",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryRequests_PopoverId",
                table: "QueryRequests",
                column: "PopoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryRequests");

            migrationBuilder.DropTable(
                name: "QueryCategories");

            migrationBuilder.DropTable(
                name: "QueryPopover");
        }
    }
}
