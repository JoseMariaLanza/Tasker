using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Tasks
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> Query(int? taskId = null);

        Task<List<TaskItem>> GetFilteredTasksAsync(IQueryable<TaskItem> query, string? term, List<int>? categories, int offset, int? limit);

        Task LoadSubTasksRecursively(TaskItem task);

        Task<TaskItem> GetTaskByIdAsync(int id);

        Task<TaskItem> CreateTaskAsync(TaskItem item, List<int>? categories);
        //Task<TaskItem> UpdateTaskAsync(TaskItem item);
        //Task<TaskItem> DeleteTaskAsync(int id);
    }
}
