using Microsoft.EntityFrameworkCore;
using Tasker.Repositories.Categories.Models;

namespace Tasker.Services.Categories
{
    public static class QueryFilters
    {
        public static IQueryable<Category> FilterByName(this IQueryable<Category> query, string term)
        {
            if (string.IsNullOrEmpty(term)) return query;
            return query.Where(t => t.Name.Contains(term));
        }

        public static IQueryable<Category> FilterByParentCategory(this IQueryable<Category> query, List<Guid> parentCategories)
        {
            return query
                .Where(t => t.SubCategories.Any(sc => parentCategories.Contains(sc.Id)));
        }
    }
}
