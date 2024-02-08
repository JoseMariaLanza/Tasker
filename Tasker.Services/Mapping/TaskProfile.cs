using AutoMapper;
using Tasker.Repositories.Tasks;
using Tasker.Repositories.Tasks.Models;
using Tasker.Services.DTO.CategoryDTOs;
using Tasker.Services.DTO.TaskDTOs;
using Tasker.Services.DTO.TaskItemCategoryDTOs;

namespace Tasker.Services.Mapping
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskItem, TaskGetDto>()
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src.SubTasks))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.TaskItemCategories.Select(tic => tic.Category)));

            CreateMap<TaskItemCategory, TaskItemCategoryGetDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Category, CategoryGetDto>();

            CreateMap<TaskItem, TaskCreateDto>();
            CreateMap<TaskCreateDto, TaskItem>()
                .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.ProrityId));
                //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories.Select(id => new TaskItemCategory { CategoryId = id })))
                //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories.Select(id => new TaskItemCategory { CategoryId = id })));
            //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories));
            //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories.Select(id => new TaskItemCategory { CategoryId = id })));
            CreateMap<TaskItem, TaskUpdateDto>();
            CreateMap<TaskUpdateDto, TaskItem>();
        }
    }
}
