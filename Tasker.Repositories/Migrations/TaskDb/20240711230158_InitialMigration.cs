using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Repositories.Migrations.TaskDb
{
    public partial class InitialMigration : Migration
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserId = table.Column<int>(type: "int", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: true),
                    ParentTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    TaskItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "DeletedAt", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId", "UpdatedAt" },
                values: new object[] { new Guid("4e29a8df-a797-44b9-8ee1-3f6d753bb3f1"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2312), null, "Training routine for body mobility", true, "Training routine", null, 1, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2312) });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "DeletedAt", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId", "UpdatedAt" },
                values: new object[] { new Guid("c9268bec-b436-4e5d-9eab-e30e228acd52"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2314), null, "Training routine for losing weight", true, "Training routine", null, 1, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2315) });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "DeletedAt", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId", "UpdatedAt" },
                values: new object[] { new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), null, new DateTime(2024, 7, 11, 20, 1, 57, 838, DateTimeKind.Local).AddTicks(9068), null, "English study (vocabulary, reading, writing, listening and speaking)", true, "English enhancement", null, null, new DateTime(2024, 7, 11, 20, 1, 57, 840, DateTimeKind.Local).AddTicks(9199) });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "DeletedAt", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("05d55d08-0132-4549-be1b-3964f9efe03f"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2305), null, "Grammar study for English improvement", true, "Advanced grammar in use", new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), 2, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2305) },
                    { new Guid("1f11511b-7e2e-49eb-961f-3a267c24b317"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2308), null, "Grammar study for English improvement", true, "Business grammar in use", new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), 3, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2309) },
                    { new Guid("4a02625a-d4e0-41e8-a205-de8ff8542d26"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(1585), null, "Extend my english vocabulary", true, "English vocabulary", new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), 1, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(1588) },
                    { new Guid("7d08ad0d-2787-4328-9c09-f2e35ae060a3"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2301), null, "Grammar study for English improvement", true, "Essential grammar in use", new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), 1, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2302) }
                });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "DeletedAt", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId", "UpdatedAt" },
                values: new object[] { new Guid("12626dda-7659-4fa8-a6c4-e58b186f1ba8"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2288), null, "Learn regular and irregular verbs", true, "Regular and Irregular verbs", new Guid("4a02625a-d4e0-41e8-a205-de8ff8542d26"), 1, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2291) });

            migrationBuilder.InsertData(
                schema: "TaskItems",
                table: "TaskItems",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "DeletedAt", "Description", "IsActive", "Name", "ParentTaskId", "PriorityId", "UpdatedAt" },
                values: new object[] { new Guid("5b5fe954-7966-4968-a6e4-0287e1661060"), null, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2297), null, "English most used vocabulary", true, "1000 words most used in english", new Guid("4a02625a-d4e0-41e8-a205-de8ff8542d26"), 2, new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2298) });

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
