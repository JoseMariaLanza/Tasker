using Tasker.Services.DTO.CategoryDTOs;
using Tasker.Services.Pagination;
using Tasker.Services.Tasks;

namespace Tasker.Services.Categories
{
    public interface ICategoryService
    {
        Task<PaginationResult<CategoryGetDto>> GetFilteredCategoriesAsync(SearchParams searchParams, PaginationParams paginationParams);
        
        Task<CategoryGetDto> GetCategoryAsync(int categoryId);

        Task<CategoryGetDto> CreateCategoryAsync(CategoryCreateDto category);

        Task<CategoryGetDto> UpdateCategoryAsync(CategoryUpdateDto category);

        Task<CategoryGetDto> DeleteCategoryAsync(int categoryId);

        //Task<TaskGetDto> UpdateTaskAsync(TaskUpdateDto task);
        //Task<TaskGetDto> DeleteTaskAsync(int taskId);
    }
}
