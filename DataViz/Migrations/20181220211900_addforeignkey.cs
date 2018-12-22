using Microsoft.EntityFrameworkCore.Migrations;

namespace DataViz.Migrations
{
    public partial class addforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryRequests_QueryCategories_CategoriesId",
                table: "QueryRequests");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "QueryRequests",
                newName: "QueryCategories");

            migrationBuilder.RenameIndex(
                name: "IX_QueryRequests_CategoriesId",
                table: "QueryRequests",
                newName: "IX_QueryRequests_QueryCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryRequests_QueryCategories_QueryCategories",
                table: "QueryRequests",
                column: "QueryCategories",
                principalTable: "QueryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryRequests_QueryCategories_QueryCategories",
                table: "QueryRequests");

            migrationBuilder.RenameColumn(
                name: "QueryCategories",
                table: "QueryRequests",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_QueryRequests_QueryCategories",
                table: "QueryRequests",
                newName: "IX_QueryRequests_CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueryRequests_QueryCategories_CategoriesId",
                table: "QueryRequests",
                column: "CategoriesId",
                principalTable: "QueryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
