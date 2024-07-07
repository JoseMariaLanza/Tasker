using Tasker.Repositories.Categories.Models;

namespace Tasker.Repositories.Categories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Query(int? categoryId = null);

        Task<List<Category>> GetFilteredCategoriesAsync(IQueryable<Category> query, string? term, int offset, int? limit);

        Task LoadSubCategoriesRecursively(Category category);

        Task<Category> GetCategoryByIdAsync(int id);

        Task<Category> CreateCategoryAsync(Category category);

        Task<Category> UpdateCategoryAsync(Category category);

        Task<Category> DeleteCategoryAsync(int categoryId);
    }
}
