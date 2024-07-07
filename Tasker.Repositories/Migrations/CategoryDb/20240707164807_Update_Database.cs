using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Repositories.Migrations.CategoryDb
{
    public partial class Update_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalSchema: "Categories",
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Categories",
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Name", "ParentCategoryId" },
                values: new object[] { 1, true, "Category 1", null });

            migrationBuilder.InsertData(
                schema: "Categories",
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Name", "ParentCategoryId" },
                values: new object[] { 2, true, "Category 2", null });

            migrationBuilder.InsertData(
                schema: "Categories",
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Name", "ParentCategoryId" },
                values: new object[] { 3, true, "Category 3", null });

            migrationBuilder.InsertData(
                schema: "Categories",
                table: "Categories",
                columns: new[] { "Id", "IsActive", "Name", "ParentCategoryId" },
                values: new object[] { 4, true, "Category 4", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                schema: "Categories",
                table: "Categories",
                column: "ParentCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Categories");
        }
    }
}
