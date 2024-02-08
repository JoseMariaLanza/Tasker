﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tasker.Repositories.Tasks;

#nullable disable

namespace Tasker.Repositories.Migrations.TaskDb
{
    [DbContext(typeof(TaskDbContext))]
    [Migration("20231212004514_InitialTaskMigration")]
    partial class InitialTaskMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tasker.Repositories.Auth.Models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AssignedUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentTaskId")
                        .HasColumnType("int");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentTaskId");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.TaskItemCategory", b =>
                {
                    b.Property<int>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("TaskItemId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("TaskItemCategory");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.TaskItem", b =>
                {
                    b.HasOne("Tasker.Repositories.Tasks.Models.TaskItem", "ParentTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("ParentTaskId");

                    b.Navigation("ParentTask");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.TaskItemCategory", b =>
                {
                    b.HasOne("Tasker.Repositories.Tasks.Models.Category", "Category")
                        .WithMany("TaskItemCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tasker.Repositories.Tasks.Models.TaskItem", "TaskItem")
                        .WithMany("TaskItemCategories")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("TaskItem");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.Category", b =>
                {
                    b.Navigation("TaskItemCategories");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.TaskItem", b =>
                {
                    b.Navigation("SubTasks");

                    b.Navigation("TaskItemCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
