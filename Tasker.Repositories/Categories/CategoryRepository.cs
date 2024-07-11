using Microsoft.EntityFrameworkCore;
using Tasker.Repositories.Categories.Models;

namespace Tasker.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryDbContext _categoryDbContext;

        public CategoryRepository(ICategoryDbContext categoryDbContext) 
        {
            _categoryDbContext = categoryDbContext;
        }

        public IQueryable<Category> Query(Guid? categoryId = null)
        {
            return _categoryDbContext.Categories
                .Where(c => c.ParentCategoryId == categoryId);
        }

        public async Task<List<Category>> GetFilteredCategoriesAsync(IQueryable<Category> query, string? term, int offset, int? limit)
        {
            query = query.Skip((offset - 1) * limit.Value).Take(limit.Value);
            var categories = await query.ToListAsync();

            return categories;

        }

        public async Task LoadSubCategoriesRecursively(Category category)
        {
            category.SubCategories = await _categoryDbContext.Categories
                                .Where(st => st.ParentCategoryId == category.Id)
                                .ToListAsync();

            foreach (var subCategory in category.SubCategories)
            {
                await LoadSubCategoriesRecursively(subCategory);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                throw new Exception("Category doen't exists");
            }
            await LoadSubCategoriesRecursively(category);

            return category;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            using var transaction = _categoryDbContext.BeginTransaction();
            try
            {
                _categoryDbContext.Categories.Add(category);
                await _categoryDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            using var transaction = _categoryDbContext.BeginTransaction();
            try
            {
                _categoryDbContext.Categories.Update(category);
                await _categoryDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                var updatedCategory = await GetCategoryByIdAsync(category.Id);

                return updatedCategory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Category> DeleteCategoryAsync(Guid categoryId)
        {
            using var transaction = _categoryDbContext.BeginTransaction();
            try
            {
                var category = await _categoryDbContext.Categories.FindAsync(categoryId);
                if (category != null)
                {
                    category.IsActive = false;
                    _categoryDbContext.Categories.Update(category);
                    await _categoryDbContext.SaveChangesAsync();
                    return category;
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
    }
}
