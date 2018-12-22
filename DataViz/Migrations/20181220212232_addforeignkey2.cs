using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataViz.Migrations
{
    public partial class addforeignkey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryRequests_QueryCategories_QueryCategories",
                table: "QueryRequests");

            migrationBuilder.DropIndex(
                name: "IX_QueryRequests_QueryCategories",
                table: "QueryRequests");

            migrationBuilder.DropColumn(
                name: "QueryCategories",
                table: "QueryRequests");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "QueryRequests",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_QueryRequests_QueryCategories_Id",
                table: "QueryRequests",
                column: "Id",
                principalTable: "QueryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryRequests_QueryCategories_Id",
                table: "QueryRequests");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "QueryRequests",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "QueryCategories",
                table: "QueryRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QueryRequests_QueryCategories",
                table: "QueryRequests",
                column: "QueryCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryRequests_QueryCategories_QueryCategories",
                table: "QueryRequests",
                column: "QueryCategories",
                principalTable: "QueryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
