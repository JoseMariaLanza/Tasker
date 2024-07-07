using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tasker.Repositories.Categories.Models;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Categories
{
    public interface ICategoryDbContext
    {
        public DbSet<Category> Categories { get; }

        //public DbSet<TaskItemCategory> TaskItemCategories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        IDbContextTransaction BeginTransaction();
    }
}
