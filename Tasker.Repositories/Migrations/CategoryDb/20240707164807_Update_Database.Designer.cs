﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tasker.Repositories.Categories;

#nullable disable

namespace Tasker.Repositories.Migrations.CategoryDb
{
    [DbContext(typeof(CategoryDbContext))]
    [Migration("20240707164807_Update_Database")]
    partial class Update_Database
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Categories")
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tasker.Repositories.Categories.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories", "Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "Category 1"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "Category 2"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Name = "Category 3"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            Name = "Category 4",
                            ParentCategoryId = 1
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