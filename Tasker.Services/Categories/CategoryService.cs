using Microsoft.AspNetCore.Http;
using AutoMapper;
using Tasker.Repositories.Categories;
using Microsoft.EntityFrameworkCore;
using Tasker.Services.Pagination;
using Tasker.Services.DTO.CategoryDTOs;
using Tasker.Repositories.Tasks.Models;
using Tasker.Repositories.Categories.Models;
using Tasker.Services.Tasks;
using System.Threading.Tasks;

namespace Tasker.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<CategoryGetDto>> GetFilteredCategoriesAsync(SearchParams searchParams, PaginationParams paginationParams)
        {
            var query = _categoryRepository.Query();

            if (searchParams.Term is not null)
            {
                query = query.FilterByName(searchParams.Term);
            }
            if (searchParams.ParentCategories is not null)
            {
                query = query.FilterByParentCategory(searchParams.ParentCategories);
            }

            var categories = await _categoryRepository.GetFilteredCategoriesAsync(query, searchParams.Term, paginationParams.Offset, paginationParams.Limit);

            foreach (var category in categories)
            {
                await _categoryRepository.LoadSubCategoriesRecursively(category);
            }

            var categoriesGetDto = _mapper.Map<List<CategoryGetDto>>(categories);

            int totalCount = await query.CountAsync();

            return new PaginationResult<CategoryGetDto>()
            {
                Data = categoriesGetDto,
                Offset = paginationParams.Offset,
                Limit = paginationParams.Limit,
                Count = totalCount,
            };
        }

        public async Task<CategoryGetDto> GetCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            if (category is null)
            {
                throw new BadHttpRequestException("Category doesn't exists.");
            }

            return _mapper.Map<CategoryGetDto>(category);
        }

        public async Task<CategoryGetDto> CreateCategoryAsync(CategoryCreateDto category)
        {
            var categoryModel = _mapper.Map<Category>(category);

            var newCategory = await _categoryRepository.CreateCategoryAsync(categoryModel);

            return _mapper.Map<CategoryGetDto>(newCategory);
        }

        public async Task<CategoryGetDto> UpdateCategoryAsync(CategoryUpdateDto category)
        {
            var depth = await CalculateDepth(category.ParentCategoryId);
            if (depth >= 64)
            {
                throw new InvalidOperationException("The category hierarchy exceeds the maximum allowed depth.");
            }

            var categoryModel = _mapper.Map<Category>(category);
            //categoryModel.Categories = taskItemCategories;

            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(categoryModel);

            var categoryItem = _mapper.Map<CategoryGetDto>(updatedCategory);

            //return _mapper.Map<CategoryGetDto>(newTask);
            return categoryItem;
        }

        public async Task<CategoryGetDto> DeleteCategoryAsync(int categoryId)
        {
            return _mapper.Map<CategoryGetDto>(await _categoryRepository.DeleteCategoryAsync(categoryId));
        }

        private async Task<int> CalculateDepth(int? parentTaskId, int currentDepth = 0)
        {
            if (!parentTaskId.HasValue)
            {
                return currentDepth;
            }

            var parentTask = await _categoryRepository.GetCategoryByIdAsync(parentTaskId.Value);
            return await CalculateDepth(parentTask.ParentCategoryId, currentDepth + 1);
        }
    }
}
