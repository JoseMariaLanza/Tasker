using AutoMapper;
using Tasker.Repositories.Categories.Models;
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
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src.SubTasks))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.TaskItemCategories.Select(tic => tic.Category)));

            CreateMap<TaskItemCategory, TaskItemCategoryGetDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<TaskItemCategoryGetDto, TaskItemCategory>();

            CreateMap<TaskCreateDto, TaskItem>()
                .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.Priority));

            CreateMap<TaskUpdateDto, TaskItem>()
                .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom((src, dest) => src.CategoryIds.Select(id => new TaskItemCategory { CategoryId = id, TaskItemId = src.Id }).ToList()));

            CreateMap<TaskItem, TaskUpdateDto>();
        }
    }
}
