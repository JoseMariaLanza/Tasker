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
    [Migration("20240711230158_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("TaskItems")
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

                    b.ToTable("UserType", "TaskItems");
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.TaskItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AssignedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentTaskId");

                    b.ToTable("TaskItems", "TaskItems");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 838, DateTimeKind.Local).AddTicks(9068),
                            Description = "English study (vocabulary, reading, writing, listening and speaking)",
                            IsActive = true,
                            Name = "English enhancement",
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 840, DateTimeKind.Local).AddTicks(9199)
                        },
                        new
                        {
                            Id = new Guid("4a02625a-d4e0-41e8-a205-de8ff8542d26"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(1585),
                            Description = "Extend my english vocabulary",
                            IsActive = true,
                            Name = "English vocabulary",
                            ParentTaskId = new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"),
                            PriorityId = 1,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(1588)
                        },
                        new
                        {
                            Id = new Guid("12626dda-7659-4fa8-a6c4-e58b186f1ba8"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2288),
                            Description = "Learn regular and irregular verbs",
                            IsActive = true,
                            Name = "Regular and Irregular verbs",
                            ParentTaskId = new Guid("4a02625a-d4e0-41e8-a205-de8ff8542d26"),
                            PriorityId = 1,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2291)
                        },
                        new
                        {
                            Id = new Guid("5b5fe954-7966-4968-a6e4-0287e1661060"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2297),
                            Description = "English most used vocabulary",
                            IsActive = true,
                            Name = "1000 words most used in english",
                            ParentTaskId = new Guid("4a02625a-d4e0-41e8-a205-de8ff8542d26"),
                            PriorityId = 2,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2298)
                        },
                        new
                        {
                            Id = new Guid("7d08ad0d-2787-4328-9c09-f2e35ae060a3"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2301),
                            Description = "Grammar study for English improvement",
                            IsActive = true,
                            Name = "Essential grammar in use",
                            ParentTaskId = new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"),
                            PriorityId = 1,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2302)
                        },
                        new
                        {
                            Id = new Guid("05d55d08-0132-4549-be1b-3964f9efe03f"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2305),
                            Description = "Grammar study for English improvement",
                            IsActive = true,
                            Name = "Advanced grammar in use",
                            ParentTaskId = new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"),
                            PriorityId = 2,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2305)
                        },
                        new
                        {
                            Id = new Guid("1f11511b-7e2e-49eb-961f-3a267c24b317"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2308),
                            Description = "Grammar study for English improvement",
                            IsActive = true,
                            Name = "Business grammar in use",
                            ParentTaskId = new Guid("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"),
                            PriorityId = 3,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2309)
                        },
                        new
                        {
                            Id = new Guid("4e29a8df-a797-44b9-8ee1-3f6d753bb3f1"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2312),
                            Description = "Training routine for body mobility",
                            IsActive = true,
                            Name = "Training routine",
                            PriorityId = 1,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2312)
                        },
                        new
                        {
                            Id = new Guid("c9268bec-b436-4e5d-9eab-e30e228acd52"),
                            CreatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2314),
                            Description = "Training routine for losing weight",
                            IsActive = true,
                            Name = "Training routine",
                            PriorityId = 1,
                            UpdatedAt = new DateTime(2024, 7, 11, 20, 1, 57, 841, DateTimeKind.Local).AddTicks(2315)
                        });
                });

            modelBuilder.Entity("Tasker.Repositories.Tasks.Models.TaskItemCategory", b =>
                {
                    b.Property<Guid>("TaskItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TaskItemId", "CategoryId");

                    b.ToTable("TaskItemCategories", "TaskItems");
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
                    b.HasOne("Tasker.Repositories.Tasks.Models.TaskItem", null)
                        .WithMany("TaskItemCategories")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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