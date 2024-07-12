using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tasker.Helpers;
using Tasker.Services.Categories;
using Tasker.Services.DTO.CategoryDTOs;
using Tasker.Services.Pagination;

namespace Tasker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get filtered categories.")]
        [SwaggerResponse(200, "Successfuly retrieved category list.", typeof(PaginationResult<CategoryGetDto>))]
        [SwaggerResponse(204, "Does not exists any category yet.")]
        public async Task<IActionResult> GetCategories([FromQuery] SearchParams searchParams, [FromQuery] PaginationParams paginationParams)
        {
            var result = await _categoryService.GetFilteredCategoriesAsync(searchParams, paginationParams);

            if (result.Count == 0) return ApiResponse.NoContent();

            return ApiResponse.Ok("Categories retrieved successfuly.", "", result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new category.")]
        [SwaggerResponse(201, "Category successfuly created.", typeof(ApiResponse))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);

            if (result == null) return ApiResponse.NoContent();

            return ApiResponse.Created("Category successfuly created.", "data", result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update an existing category.")]
        [SwaggerResponse(200, "Category successfuly updated.", typeof(ApiResponse))]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto category)
        {
            var result = await _categoryService.UpdateCategoryAsync(category);

            if (result is null) return ApiResponse.NoContent();

            return ApiResponse.Ok("Category successfuly updated", "data", result);
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Logic delete an existing category.")]
        [SwaggerResponse(200, "Category successfuly deleted.", typeof(ApiResponse))]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(categoryId);

            if (result is null) return ApiResponse.NotFound("Category does not exists.");

            return ApiResponse.Ok("Category successfuly deleted", "data", result);
        }
    }
}
