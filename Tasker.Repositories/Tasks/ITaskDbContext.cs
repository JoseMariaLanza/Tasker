using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Tasks
{
    public interface ITaskDbContext
    {
        public DbSet<TaskItem> TaskItems { get; }

        public DbSet<TaskItemCategory> TaskItemCategories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        IDbContextTransaction BeginTransaction();
    }
}
