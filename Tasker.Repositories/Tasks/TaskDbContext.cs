using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tasker.Repositories.Auth.Models;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Tasks
{
    public class TaskDbContext : DbContext, ITaskDbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        
        public DbSet<TaskItemCategory> TaskItemCategories { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
                .HasMany(t => t.SubTasks)
                .WithOne(t => t.ParentTask)
                .HasForeignKey(t => t.ParentTaskId)
                .IsRequired(false);

            // RENAME TaskItemCatogory TABLE to TaskItemCategories
            modelBuilder.Entity<TaskItemCategory>().ToTable("TaskItemCategories");

            // Composite key - TaskItemCategory
            modelBuilder.Entity<TaskItemCategory>()
                .HasKey(t => new { t.TaskItemId, t.CategoryId });

            // Many to many
            // modelBuilder.Entity<TaskItemCategory>();
                //.HasOne(pt => pt.TaskItem)
                //.WithMany(p => p.TaskItemCategories)
                //.HasForeignKey(pt => pt.TaskItemId);

            //modelBuilder.Entity<TaskItemCategory>();
                //.HasOne(pt => pt.Category)
                //.WithMany(t => t.TaskItemCategories)
                //.HasForeignKey(pt => pt.CategoryId);

            modelBuilder.Ignore<User>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}
