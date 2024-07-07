using AutoMapper;
using Tasker.Repositories.Categories.Models;
using Tasker.Repositories.Tasks;
using Tasker.Repositories.Tasks.Models;
using Tasker.Services.DTO.CategoryDTOs;
using Tasker.Services.DTO.TaskDTOs;
using Tasker.Services.DTO.TaskItemCategoryDTOs;

namespace Tasker.Services.Mapping
{
    public class CategorProfile : Profile
    {
        public CategorProfile()
        {

            // Task Mapping
            CreateMap<Category, CategoryGetDto>()
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));

            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
