using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Repositories.Migrations.TaskDb
{
    public partial class Update_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TaskItems");

            migrationBuilder.CreateTable(
                name: "TaskItems",
                schema: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserId = table.Column<int>(type: "int", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: true),
                    ParentTaskId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_TaskItems_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalSchema: "TaskItems",
                        principalTable: "TaskItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                schema: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskItemCategories",
                schema: "TaskItems",
                columns: table => new
                {
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItemCategories", x => new { x.TaskItemId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_TaskItemCategories_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalSchema: "TaskItems",
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId" },
                values: new object[] { 1, null, "Task item 1 description", true, "Task item 1", null, 0 });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId" },
                values: new object[] { 2, null, "Task item 2 description", true, "Task item 2", null, 2 });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId" },
                values: new object[] { 5, null, "Task item 5 description", true, "Task item 5", null, 1 });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId" },
                values: new object[] { 3, null, "Task item 3 description", true, "Task item 3", 1, 4 });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId" },
                values: new object[] { 6, null, "Task item 6 description", true, "Task item 6", 1, 5 });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId" },
                values: new object[] { 4, null, "Task item 4 description", true, "Task item 4", 6, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ParentTaskId",
                schema: "TaskItems",
                table: "TaskItems",
                column: "ParentTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItemCategories",
                schema: "TaskItems");

            migrationBuilder.DropTable(
                name: "UserType",
                schema: "TaskItems");

            migrationBuilder.DropTable(
                name: "TaskItems",
                schema: "TaskItems");
        }
    }
}
