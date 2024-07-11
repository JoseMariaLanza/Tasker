using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Repositories.Migrations.CategoryDb
{
    public partial class InitialMigration : Migration
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[] { new Guid("3e2b8a14-0b3f-4f92-ba71-0325fdff998c"), new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1571), null, true, "Relationship", null, new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1572) });

            migrationBuilder.InsertData(
                schema: "Categories",
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[] { new Guid("823b62a1-45e3-4daa-b398-7563e5178cec"), new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1559), null, true, "Health", null, new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1562) });

            migrationBuilder.InsertData(
                schema: "Categories",
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[] { new Guid("dd1fa9ba-844a-4835-8479-b8a35926d240"), new DateTime(2024, 7, 11, 20, 2, 17, 30, DateTimeKind.Local).AddTicks(9289), null, true, "Professional growth", null, new DateTime(2024, 7, 11, 20, 2, 17, 32, DateTimeKind.Local).AddTicks(9600) });

            migrationBuilder.InsertData(
                schema: "Categories",
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsActive", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[] { new Guid("cdd240ea-6013-4970-8115-c27809408d1f"), new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1574), null, true, "Mindfulness", new Guid("823b62a1-45e3-4daa-b398-7563e5178cec"), new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1574) });

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
