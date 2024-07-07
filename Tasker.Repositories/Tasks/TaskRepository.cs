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

        public IQueryable<TaskItem> Query(int? taskId = null)
        {
            //var categories = _categoryDbContext.TaskItemCategories.Contains(tic => tic.taskId == taskId);
            return _taskDbContext.TaskItems
                    .Include(t => t.SubTasks)
                        .ThenInclude(st => st.SubTasks)
                    .Where(t => t.ParentTaskId == taskId);


            //return _taskDbContext.TaskItems
            //       .Include(t => t.SubTasks)
            //           .ThenInclude(st => st.SubTasks)
            //       .Include(ct => ct.TaskItemCategories)
            //           .ThenInclude(c => c.Category)
            //       .Where(t => t.ParentTaskId == taskId);


            //.Include(ct => ct.Categories)
            //            .ThenInclude(c => c.Category)
            //            .Where(t => t.ParentTaskId == taskId);


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
                                .ToListAsync();

            //task.SubTasks = await _taskDbContext.TaskItems
            //                    .Where(st => st.ParentTaskId == task.Id)
            //                    .Include(ct => ct.TaskItemCategories)
            //                        .ThenInclude(c => c.Category)
            //                    .ToListAsync();

            // Recursively load subtasks for each subtask
            foreach (var subTask in task.SubTasks)
            {
                await LoadSubTasksRecursively(subTask);
            }
        }

        public async Task<List<Category>> LoadTasksCategoriesAsync(int taskId, List<int>? categoryIds)
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
                //task.Categories = categories;

                //foreach (var tic  in taskItemCategories)
                //{
                //    tic.Category = await _categoryDbContext.Categories
                //        .Where(c => categories.Contains(c.Id)).ToListAsync();
                //}
            }

            return null;

            //var taskCategories = await _categoryDbContext.TaskItemCategories
            //    //.Where(tc => tc.TaskItemId == task.Id || categoryIds.Contains(tc.CategoryId))
            //    .Where(tc => tc.TaskItemId == task.Id)
            //        //.Include(tc => tc.Category)
            //    .ToListAsync();

            //task.TaskItemCategories = taskCategories;
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
            var task = await _taskDbContext.TaskItems.Include(ct => ct.TaskItemCategories)
                .ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (task is null)
            {
                throw new Exception("Task doen't exists");
            }
            await LoadSubTasksRecursively(task);

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

        public async Task<TaskItem> UpdateTaskAsync(TaskItem task, List<TaskItemCategory>? categories)
        {
            using var transaction = _taskDbContext.BeginTransaction();
            try
            {
                _taskDbContext.TaskItems.Update(task);
                //await _taskDbContext.SaveChangesAsync();

                //var taskItemCategories = categories.Distinct().Select(categoryId => new TaskItemCategory
                //{
                //    TaskItemId = task.Id,
                //    CategoryId = categoryId
                //}).ToList();

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

                //task.Categories = taskItemCategories;
                //var updatedTask = await Query(task.Id).FirstOrDefaultAsync();
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

        public async Task<TaskItem> DeleteTaskAsync(int taskId)
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
