using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ITaskDbContext _taskDbContext;

        public TaskRepository(ITaskDbContext taskDbContext) 
        {
            _taskDbContext = taskDbContext;
        }

        public IQueryable<TaskItem> Query(int? taskId = null)
        {
            return _taskDbContext.TaskItems
                    .Include(t => t.SubTasks)
                        .ThenInclude(st => st.SubTasks)
                    .Include(ct => ct.TaskItemCategories)
                        .ThenInclude(c => c.Category)
                        .Where(t => t.ParentTaskId == taskId);

            //var topLevelTasks = _taskDbContext.TaskItems
            //    .Where(t => t.ParentTaskId == null)
            //    .Include(t => t.TaskItemCategories)
            //        .ThenInclude(tic => tic.Category);

            //foreach (var task in topLevelTasks)
            //{
            //    task.SubTasks = task.LoadSubTasksAsync(task.Id);
            //}

            //return topLevelTasks;
        }

        //private async Task<List<TaskItem>> LoadSubTasks(List<TaskItem> tasks)
        //private async Task<TaskItem> LoadSubTasks(TaskItem taskItem)
        //{
        //    var subTasks = await Query(taskItem.Id).ToListAsync();

        //    foreach (var item in subTasks)
        //    {
        //        if (subTasks.Any())
        //        {
        //            return await LoadSubTasks(item);
        //        }
        //    }
        //    taskItem.SubTasks = subTasks;
        //    return taskItem;
        //}

        private async Task LoadSubTasks(TaskItem taskItem)
        {
            var subTasks = await Query(taskItem.Id).ToListAsync();
            taskItem.SubTasks = new List<TaskItem>();

            foreach (var item in subTasks)
            {
                await LoadSubTasks(item);
                taskItem.SubTasks.Add(item);
            }
        }



        //public async Task<List<TaskItem>> GetTasksWithSubtasksAsync(int? parentId = null)
        //{
        //    // Eagerly load all tasks and their categories
        //    var allTasks = await _taskDbContext.TaskItems
        //        .Include(t => t.TaskItemCategories)
        //            .ThenInclude(tic => tic.Category)
        //        .ToListAsync();

        //    // Organize them into a hierarchy
        //    return allTasks.Where(t => t.ParentTaskId == parentId).ToList();
        //}

        //public void LoadSubtasks(List<TaskItem> tasks, List<TaskItem> allTasks)
        //{
        //    foreach (var task in tasks)
        //    {
        //        task.SubTasks = allTasks.Where(t => t.ParentTaskId == task.Id).ToList();
        //        LoadSubtasks(task.SubTasks, allTasks); // Recursive call to set subtasks for each task
        //    }
        //}

        //private IQueryable<TaskItem> LoadSubTasksAsync(int taskItemId)
        //{
        //    // Load the immediate subtasks
        //    var subTasks = _taskDbContext.TaskItems
        //        .Where(t => t.ParentTaskId == taskItemId)
        //        .Include(t => t.TaskItemCategories)
        //            .ThenInclude(tic => tic.Category);

        //    // Iterate over subtasks and load their subtasks
        //    foreach (var subTask in subTasks)
        //    {
        //        subTask.SubTasks = LoadSubTasksAsync(subTask.Id);
        //    }

        //    return subTasks;
        //}

        public async Task<List<TaskItem>> GetFilteredTasksAsync(IQueryable<TaskItem> query, string? term, List<int>? categories, int offset, int? limit)
        {
            query = query.Skip((offset - 1) * limit.Value).Take(limit.Value);
            var tasks = await query.ToListAsync();

            //foreach (var task in tasks)
            //{
            //    if (task.SubTasks.Any())
            //    {
            //        await LoadSubTasks(task);
            //    }
            //}

            return tasks;
            //var tasks = await query.ToListAsync();

            //foreach (var task in tasks)
            //{
            //    if (task.SubTasks.Any())
            //    {
            //        //await LoadSubTasks(task);
            //        query = query.Append(task);
            //        tasks = await query.ToListAsync();  
            //    }
            //}

            //return tasks;

        }

        public async Task LoadSubTasksRecursively(TaskItem task)
        {
            // Load the immediate subtasks for the task
            task.SubTasks = await _taskDbContext.TaskItems
                                .Where(st => st.ParentTaskId == task.Id)
                                .Include(ct => ct.TaskItemCategories)
                                    .ThenInclude(c => c.Category)
                                .ToListAsync();

            // Recursively load subtasks for each subtask
            foreach (var subTask in task.SubTasks)
            {
                await LoadSubTasksRecursively(subTask);
            }
        }


        //public async Task<List<TaskItem>> GetFilteredTasksAsync(SearchParams searchParams)
        //{
        //    var query = Query();

        //    query = query.FilterByNameOrDescription(searchParams.Term)
        //                 .FilterByCategories(searchParams.Categories);

        //    return await query.ToListAsync();
        //}

        //Task<IQueryable<TaskItem>> GetFilteredTasksAsync()
        //{
        //    return Task.FromResult(_taskDbContext.Tasks.AsQueryable());
        //}

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            var task = await _taskDbContext.TaskItems.FindAsync(id);
            if (task is null)
            {
                throw new Exception("Task doen't exists");
            }

            return task;
        }
        public async Task<TaskItem> CreateTaskAsync(TaskItem task, List<int>? categories)
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

        //Task<TaskItem> UpdateTaskAsync(TaskItem item)
        //{
        //}
        //Task<TaskItem> DeleteTaskAsync(int id)
        //{
        //    // TODO: Apply transaction to delete subtasks, then delete parent task
        //}
    }
}
