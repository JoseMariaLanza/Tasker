using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Repositories.Migrations.TaskDb
{
    public partial class RenameTaskItemCategoryToTaskItemCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItemCategory_Category_CategoryId",
                table: "TaskItemCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItemCategory_TaskItems_TaskItemId",
                table: "TaskItemCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItemCategory",
                table: "TaskItemCategory");

            migrationBuilder.RenameTable(
                name: "TaskItemCategory",
                newName: "TaskItemCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItemCategory_CategoryId",
                table: "TaskItemCategories",
                newName: "IX_TaskItemCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItemCategories",
                table: "TaskItemCategories",
                columns: new[] { "TaskItemId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItemCategories_Category_CategoryId",
                table: "TaskItemCategories",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItemCategories_TaskItems_TaskItemId",
                table: "TaskItemCategories",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItemCategories_Category_CategoryId",
                table: "TaskItemCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItemCategories_TaskItems_TaskItemId",
                table: "TaskItemCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItemCategories",
                table: "TaskItemCategories");

            migrationBuilder.RenameTable(
                name: "TaskItemCategories",
                newName: "TaskItemCategory");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItemCategories_CategoryId",
                table: "TaskItemCategory",
                newName: "IX_TaskItemCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItemCategory",
                table: "TaskItemCategory",
                columns: new[] { "TaskItemId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItemCategory_Category_CategoryId",
                table: "TaskItemCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItemCategory_TaskItems_TaskItemId",
                table: "TaskItemCategory",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
