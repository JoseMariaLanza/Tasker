using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tasker.Repositories.Auth.Models;
using Tasker.Repositories.Categories.Models;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Categories
{
    public class CategoryDbContext : DbContext, ICategoryDbContext
    {
        public DbSet<Category> Categories { get; set; }

        //public DbSet<TaskItemCategory> TaskItemCategories { get; set; }

        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Categories");

            modelBuilder.Entity<Category>().HasData(CategorySeed);
            //modelBuilder.Entity<TaskItemCategory>().HasData(TaskItemCategorySeed);

            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.TaskItemCategories)
            //    .WithOne(tc => tc.Category)
            //    .HasForeignKey(tc => tc.CategoryId);

            //modelBuilder.Entity<TaskItemCategory>().ToTable("TaskItemCategories")
            //    .HasKey(t => new { t.TaskItemId, t.CategoryId });

            //modelBuilder.Entity<TaskItemCategory>()
            //    .HasKey(t => new { t.TaskItemId, t.CategoryId });

            modelBuilder.Ignore<User>();
            modelBuilder.Ignore<TaskItemCategory>();

            //modelBuilder.Entity<Category>()
            //    .HasMany(t => t.SubCategories)
            //    .WithOne(t => t.ParentCategory)
            //    .HasForeignKey(t => t.ParentCategoryId)
            //    .IsRequired(false);

            ////modelBuilder.Entity<TaskItem>()
            ////    .HasMany(t => t.Categories)
            ////    .WithOne(tc => tc.TaskItem)
            ////    .HasForeignKey(tc => tc.TaskItemId);

            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.TaskItemCategories)
            //    .WithOne(tc => tc.Category)
            //    .HasForeignKey(tc => tc.CategoryId);

            //modelBuilder.Ignore<TaskItemCategory>();

            ////modelBuilder.Entity<TaskItemCategory>().ToTable("TaskItemCategories");

            ////// Composite key - TaskItemCategory
            ////modelBuilder.Entity<TaskItemCategory>()
            ////    .HasKey(t => new { t.TaskItemId, t.CategoryId });

            //modelBuilder.Ignore<User>();
            //modelBuilder.Ignore<UserType>();
        }

        private static readonly Category[] CategorySeed =
        {
            new Category { Id = Guid.Parse("dd1fa9ba-844a-4835-8479-b8a35926d240"), Name = "Professional growth", ParentCategoryId = null, IsActive = true },
            new Category { Id = Guid.Parse("823b62a1-45e3-4daa-b398-7563e5178cec"), Name = "Health", ParentCategoryId = null, IsActive = true },
            new Category { Id = Guid.Parse("3e2b8a14-0b3f-4f92-ba71-0325fdff998c"), Name = "Relationship", ParentCategoryId = null, IsActive = true },
            new Category { Id = Guid.Parse("cdd240ea-6013-4970-8115-c27809408d1f"), Name = "Mindfulness", ParentCategoryId = Guid.Parse("823b62a1-45e3-4daa-b398-7563e5178cec"), IsActive = true },
        };

        //private static readonly TaskItemCategory[] TaskItemCategorySeed = {
        //    new TaskItemCategory { TaskItemId = 1, CategoryId = 1 },
        //    new TaskItemCategory { TaskItemId = 3, CategoryId = 4 },
        //};

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}
