using Microsoft.EntityFrameworkCore;
using Tasker.Repositories.Tasks.Models;

namespace Tasker.Repositories.Tasks
{
    public static class QueryFilters
    {
        public static IQueryable<TaskItem> FilterByNameOrDescription(this IQueryable<TaskItem> query, string term)
        {
            if (string.IsNullOrEmpty(term)) return query;
            return query
                //.Include(tc => tc.TaskItemCategories)
                //.ThenInclude(c => c.Category)
                .Where(t => t.Name.Contains(term) || t.Description.Contains(term));
        }

        public static IQueryable<TaskItem> FilterByCategories(this IQueryable<TaskItem> query, List<int> categories)
        {
            if (categories is null || !categories.Any()) return query;
            return query
                //.Include(tc => tc.TaskItemCategories)
                //.ThenInclude(c => c.Category)
                .Where(t => t.TaskItemCategories.Any(tc => categories.Contains(tc.CategoryId)));
        }

        public static IQueryable<TaskItem> LoadSubTasksAsync(this IQueryable<TaskItem> query, TaskItem taskItem)
        {
            query
                .Where(t => t.ParentTaskId == taskItem.Id)
                    .Include(t => t.SubTasks)
                        .ThenInclude(st => st.SubTasks);

            //query
            //    .Where(t => t.ParentTaskId == taskItem.Id)
            //    .Include(t => t.TaskItemCategories)
            //        .ThenInclude(tic => tic.Category).ToList();

            //foreach (var subTask in query)
            //{
            //    query.LoadSubTasksAsync(subTask);
            //}

            return query;
        }
    }
}
