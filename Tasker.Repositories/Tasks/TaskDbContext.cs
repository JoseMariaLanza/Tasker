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
            new TaskItem { Id = 1, Name = "Task item 1", Description = "Task item 1 description", ParentTaskId = null, PriorityId = 0, IsActive = true},
            new TaskItem { Id = 2, Name = "Task item 2", Description = "Task item 2 description", ParentTaskId = null, PriorityId = 2, IsActive = true},
            new TaskItem { Id = 3, Name = "Task item 3", Description = "Task item 3 description", ParentTaskId = 1, PriorityId = 4, IsActive = true},
            new TaskItem { Id = 4, Name = "Task item 4", Description = "Task item 4 description", ParentTaskId = 6, PriorityId = 1, IsActive = true},
            new TaskItem { Id = 5, Name = "Task item 5", Description = "Task item 5 description", ParentTaskId = null, PriorityId = 1, IsActive = true},
            new TaskItem { Id = 6, Name = "Task item 6", Description = "Task item 6 description", ParentTaskId = 1, PriorityId = 5, IsActive = true},
        };

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}
