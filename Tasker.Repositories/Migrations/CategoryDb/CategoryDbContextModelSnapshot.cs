﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tasker.Repositories.Categories;

#nullable disable

namespace Tasker.Repositories.Migrations.CategoryDb
{
    [DbContext(typeof(CategoryDbContext))]
    partial class CategoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Categories")
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tasker.Repositories.Categories.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories", "Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dd1fa9ba-844a-4835-8479-b8a35926d240"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 30, DateTimeKind.Local).AddTicks(9289),
                            IsActive = true,
                            Name = "Professional growth",
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 32, DateTimeKind.Local).AddTicks(9600)
                        },
                        new
                        {
                            Id = new Guid("823b62a1-45e3-4daa-b398-7563e5178cec"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1559),
                            IsActive = true,
                            Name = "Health",
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1562)
                        },
                        new
                        {
                            Id = new Guid("3e2b8a14-0b3f-4f92-ba71-0325fdff998c"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1571),
                            IsActive = true,
                            Name = "Relationship",
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1572)
                        },
                        new
                        {
                            Id = new Guid("cdd240ea-6013-4970-8115-c27809408d1f"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1574),
                            IsActive = true,
                            Name = "Mindfulness",
                            ParentCategoryId = new Guid("823b62a1-45e3-4daa-b398-7563e5178cec"),
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 2, 17, 33, DateTimeKind.Local).AddTicks(1574)
                        });
                });

            modelBuilder.Entity("Tasker.Repositories.Categories.Models.Category", b =>
                {
                    b.HasOne("Tasker.Repositories.Categories.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Tasker.Repositories.Categories.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
