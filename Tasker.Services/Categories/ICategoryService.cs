using Tasker.Services.DTO.CategoryDTOs;
using Tasker.Services.Pagination;
using Tasker.Services.Tasks;

namespace Tasker.Services.Categories
{
    public interface ICategoryService
    {
        Task<PaginationResult<CategoryGetDto>> GetFilteredCategoriesAsync(SearchParams searchParams, PaginationParams paginationParams);
        
        Task<CategoryGetDto> GetCategoryAsync(Guid categoryId);

        Task<CategoryGetDto> CreateCategoryAsync(CategoryCreateDto category);

        Task<CategoryGetDto> UpdateCategoryAsync(CategoryUpdateDto category);

        Task<CategoryGetDto> DeleteCategoryAsync(Guid categoryId);
    }
}
