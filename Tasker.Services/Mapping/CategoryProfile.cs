using AutoMapper;
using Tasker.Repositories.Categories.Models;
using Tasker.Services.DTO.CategoryDTOs;

namespace Tasker.Services.Mapping
{
    public class CategorProfile : Profile
    {
        public CategorProfile()
        {
            CreateMap<Category, CategoryGetDto>()
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));

            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
