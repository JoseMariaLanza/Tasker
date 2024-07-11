using Tasker.Repositories.Categories.Models;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Tasks
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> Query(Guid? taskId = null);

        Task<List<TaskItem>> GetFilteredTasksAsync(IQueryable<TaskItem> query, string? term, List<Guid>? categories, int offset, int? limit);

        Task LoadSubTasksRecursively(TaskItem task);

        Task<List<Category>> LoadTasksCategoriesAsync(Guid taskId, List<Guid>? categoryIds);

        Task<TaskItem> GetTaskByIdAsync(Guid id);

        Task<TaskItem> CreateTaskAsync(TaskItem item, List<Guid>? categories);

        Task<TaskItem> UpdateTaskAsync(TaskItem task, List<TaskItemCategory>? categories);

        Task<TaskItem> DeleteTaskAsync(Guid taskId);

        //Task<TaskItem> UpdateTaskAsync(TaskItem item);
        //Task<TaskItem> DeleteTaskAsync(int id);
    }
}
