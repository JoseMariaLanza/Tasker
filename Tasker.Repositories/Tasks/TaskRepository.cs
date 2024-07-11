using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Threading.Tasks;
using Tasker.Repositories.Categories;
using Tasker.Repositories.Tasks.Models;
using Tasker.Repositories.Categories.Models;

namespace Tasker.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ITaskDbContext _taskDbContext;
        private readonly ICategoryDbContext _categoryDbContext;
        private readonly ICategoryRepository _categoryRepository;

        public TaskRepository(
            ITaskDbContext taskDbContext,
            ICategoryDbContext categoryDbContext,
            ICategoryRepository categoryRepository) 
        {
            _taskDbContext = taskDbContext;
            _categoryDbContext = categoryDbContext;
            _categoryRepository = categoryRepository;
        }

        //public IQueryable<TaskItem> Query(bool asTreeView, int? taskId = null)
        public IQueryable<TaskItem> Query(Guid? taskId = null)
        {
            var allTasks = _taskDbContext.TaskItems;

            return allTasks.Where(t => t.ParentTaskId == taskId);
        }

        public async Task<List<TaskItem>> GetFilteredTasksAsync(IQueryable<TaskItem> query, string? term, List<Guid>? categories, int offset, int? limit)
        {
            query = query.Skip((offset - 1) * limit.Value).Take(limit.Value);
            var tasks = await query.ToListAsync();

            return tasks;

        }

        public async Task LoadSubTasksRecursively(TaskItem task)
        {
            // Load the immediate subtasks for the task
            task.SubTasks = await _taskDbContext.TaskItems
                                .Where(st => st.ParentTaskId == task.Id)
                                .Include(ct => ct.TaskItemCategories)
                                .ToListAsync();

            // Recursively load subtasks for each subtask
            foreach (var subTask in task.SubTasks)
            {
                await LoadSubTasksRecursively(subTask);
            }
        }

        public async Task<List<Category>> LoadTasksCategoriesAsync(Guid taskId, List<Guid>? categoryIds)
        {
            var taskItemCategories = await _taskDbContext.TaskItemCategories
                .Where(tic => tic.TaskItemId == taskId).ToListAsync();

            if (taskItemCategories.Any())
            {
                var categories = _categoryDbContext.Categories
                    .Where(c => taskItemCategories.Select(x => x.CategoryId).Contains(c.Id))
                    .ToList();

                foreach (var category in categories)
                {
                    await _categoryRepository.LoadSubCategoriesRecursively(category);
                }

                return categories;
            }

            return null;
        }

        public async Task<TaskItem> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskDbContext.TaskItems
                .Include(ct => ct.TaskItemCategories)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (task is null)
            {
                throw new Exception("Task doen't exists");
            }

            await LoadSubTasksRecursively(task);

            return task;
        }
        public async Task<TaskItem> CreateTaskAsync(TaskItem task, List<Guid>? categories)
        {
            using var transaction = _taskDbContext.BeginTransaction();
            try
            {
                _taskDbContext.TaskItems.Add(task);
                await _taskDbContext.SaveChangesAsync();

                var taskItemCategories = categories.Distinct().Select(categoryId => new TaskItemCategory
                {
                    TaskItemId = task.Id,
                    CategoryId = categoryId
                }).ToList();

                _taskDbContext.TaskItemCategories.AddRange(taskItemCategories);
                await _taskDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                task.TaskItemCategories = taskItemCategories;

                return task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task, List<TaskItemCategory>? categories)
        {
            using var transaction = _taskDbContext.BeginTransaction();
            try
            {
                _taskDbContext.TaskItems.Update(task);

                var assignedCategories = _taskDbContext.TaskItemCategories.Where(x => x.TaskItemId == task.Id);
                if (assignedCategories is not null)
                {
                    _taskDbContext.TaskItemCategories.RemoveRange(assignedCategories.ToList());
                    await _taskDbContext.SaveChangesAsync();
                }

                if (categories is not null)
                {
                    await _taskDbContext.TaskItemCategories.AddRangeAsync(categories);
                    await _taskDbContext.SaveChangesAsync();
                }
                await _taskDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                var updatedTask = await GetTaskByIdAsync(task.Id);

                return updatedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TaskItem> DeleteTaskAsync(Guid taskId)
        {
            using var transaction = _taskDbContext.BeginTransaction();
            try
            {
                var taskItem = await _taskDbContext.TaskItems.FindAsync(taskId);
                if (taskItem != null)
                {
                    taskItem.IsActive = false;
                    _taskDbContext.TaskItems.Update(taskItem);
                    await _taskDbContext.SaveChangesAsync();
                    return taskItem;
                }
                return null;
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await transaction.RollbackAsync();
                throw;
            }
        }

        //Task<TaskItem> UpdateTaskAsync(TaskItem item)
        //{
        //}
        //Task<TaskItem> DeleteTaskAsync(int id)
        //{
        //    // TODO: Apply transaction to delete subtasks, then delete parent task
        //}
    }
}
