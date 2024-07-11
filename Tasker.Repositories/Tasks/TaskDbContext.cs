using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tasker.Repositories.Auth.Models;
//using Tasker.Repositories.Categories;
//using Tasker.Repositories.Categories.Models;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Tasks
{
    public class TaskDbContext : DbContext, ITaskDbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        
        //public DbSet<Category> Categories { get; set; }

        public DbSet<TaskItemCategory> TaskItemCategories { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("TaskItems");

            modelBuilder.Entity<TaskItem>()
                .HasMany(t => t.SubTasks)
                .WithOne(t => t.ParentTask)
                .HasForeignKey(t => t.ParentTaskId)
                .IsRequired(false);

            modelBuilder.Entity<TaskItem>()
                .HasMany(ti => ti.TaskItemCategories)
                .WithOne().HasForeignKey(t => t.TaskItemId);

            modelBuilder.Entity<TaskItem>().HasData(TaskItemSeed);

            //modelBuilder.Entity<TaskItem>()
            //    .HasMany(t => t.Categories)
            //    .WithOne(tc => tc.TaskItem)
            //    .HasForeignKey(tc => tc.TaskItemId);

            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.TaskItemCategories)
            //    .WithOne(tc => tc.Category)
            //    .HasForeignKey(tc => tc.CategoryId);

            // RENAME TaskItemCategory TABLE to TaskItemCategories
            modelBuilder.Entity<TaskItemCategory>().ToTable("TaskItemCategories");

            // Composite key - TaskItemCategory
            modelBuilder.Entity<TaskItemCategory>()
                .HasKey(t => new { t.TaskItemId, t.CategoryId });

            //modelBuilder.Ignore<Category>();
            modelBuilder.Ignore<User>();

            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<TaskItem>()
            //    .HasMany(t => t.SubTasks)
            //    .WithOne(t => t.ParentTask)
            //    .HasForeignKey(t => t.ParentTaskId)
            //    .IsRequired(false);

            //modelBuilder.Entity<TaskItem>()
            //    .HasMany(c => c.Categories)
            //    .WithOne(c => c.TaskItem)
            //    .HasForeignKey(c => c.TaskItemId);

            //// RENAME TaskItemCatogory TABLE to TaskItemCategories
            //modelBuilder.Entity<TaskItemCategory>().ToTable("TaskItemCategories");

            //// Composite key - TaskItemCategory
            //modelBuilder.Entity<TaskItemCategory>()
            //    .HasKey(t => new { t.TaskItemId, t.CategoryId });

            //// Many to many
            //modelBuilder.Entity<TaskItemCategory>()
            //    .HasOne(pt => pt.TaskItem)
            //    .WithMany(p => p.Categories)
            //    .HasForeignKey(pt => pt.TaskItemId);

            //modelBuilder.Entity<TaskItemCategory>()
            //    .HasOne(pt => pt.Category)
            //    .WithMany(t => t.TaskItemCategories)
            //    .HasForeignKey(pt => pt.CategoryId);

            //modelBuilder.Ignore<User>();
        }

        private static readonly TaskItem[] TaskItemSeed = {
            new TaskItem { Id = Guid.Parse("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), Name = "English enhancement", Description = "English study (vocabulary, reading, writing, listening and speaking)", ParentTaskId = null, PriorityId = 0},
            new TaskItem { Id = Guid.Parse("4a02625a-d4e0-41e8-a205-de8ff8542d26"), Name = "English vocabulary", Description = "Extend my english vocabulary", ParentTaskId = Guid.Parse("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), PriorityId = 1},
            new TaskItem { Id = Guid.Parse("12626dda-7659-4fa8-a6c4-e58b186f1ba8"), Name = "Regular and Irregular verbs", Description = "Learn regular and irregular verbs", ParentTaskId = Guid.Parse("4a02625a-d4e0-41e8-a205-de8ff8542d26") , PriorityId = 1},
            new TaskItem { Id = Guid.Parse("5b5fe954-7966-4968-a6e4-0287e1661060"), Name = "1000 words most used in english", Description = "English most used vocabulary", ParentTaskId = Guid.Parse("4a02625a-d4e0-41e8-a205-de8ff8542d26"), PriorityId = 2},
            new TaskItem { Id = Guid.Parse("7d08ad0d-2787-4328-9c09-f2e35ae060a3"), Name = "Essential grammar in use", Description = "Grammar study for English improvement", ParentTaskId = Guid.Parse("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), PriorityId = 1},
            new TaskItem { Id = Guid.Parse("05d55d08-0132-4549-be1b-3964f9efe03f"), Name = "Advanced grammar in use", Description = "Grammar study for English improvement", ParentTaskId = Guid.Parse("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), PriorityId = 2},
            new TaskItem { Id = Guid.Parse("1f11511b-7e2e-49eb-961f-3a267c24b317"), Name = "Business grammar in use", Description = "Grammar study for English improvement", ParentTaskId = Guid.Parse("d180d2b3-9273-4a1b-87f3-a0218fdfc53e"), PriorityId = 3},
            new TaskItem { Id = Guid.Parse("4e29a8df-a797-44b9-8ee1-3f6d753bb3f1"), Name = "Training routine", Description = "Training routine for body mobility", PriorityId = 1},
            new TaskItem { Id = Guid.Parse("c9268bec-b436-4e5d-9eab-e30e228acd52"), Name = "Training routine", Description = "Training routine for losing weight", PriorityId = 1},
        };

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}
