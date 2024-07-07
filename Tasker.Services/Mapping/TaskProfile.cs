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

            // Task Mapping
            CreateMap<TaskItem, TaskGetDto>()
                //.ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.PriorityId))
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src.SubTasks))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.TaskItemCategories.Select(tic => tic.Category)));

            CreateMap<TaskItemCategory, TaskItemCategoryGetDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<TaskItemCategoryGetDto, TaskItemCategory>();
            //.ForMember(dest => dest.TaskItemId, opt => opt.MapFrom(src => src.))
            //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

            //CreateMap<TaskItem, TaskCreateDto>();
            CreateMap<TaskCreateDto, TaskItem>()
                .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.Priority));

            //CreateMap<TaskCreatedDto, TaskItem>()
            //    .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.Priority))
            //    .ForMember(dest => dest.Categories, opt => opt.MapFrom((src, dest) => src.CategoryIds.Select(id => new TaskItemCategory { CategoryId = id, TaskItemId = src.Id }).ToList()));

            //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories.Select(id => new TaskItemCategory { CategoryId = id })))
            //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories.Select(id => new TaskItemCategory { CategoryId = id })));
            //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories));
            //.ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom(src => src.Categories.Select(id => new TaskItemCategory { CategoryId = id })));

            CreateMap<TaskUpdateDto, TaskItem>()
                .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.TaskItemCategories, opt => opt.MapFrom((src, dest) => src.CategoryIds.Select(id => new TaskItemCategory { CategoryId = id, TaskItemId = src.Id }).ToList()));

            //CreateMap<TaskUpdateDto, TaskItem>()
            //    .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.ProrityId))
            //    .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new TaskItemCategory { TaskItemId = src.Id, CategoryId = id }).ToList()));

            //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new TaskItemCategory { CategoryId = id, TaskItemId = src.Id }).ToList()));
            //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryIds.Select(tic => tic.Cate));
            //.ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src.SubTasks))
            //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
            CreateMap<TaskItem, TaskUpdateDto>();
            //.ForMember(dest => dest.ProrityId, opt => opt.MapFrom(src => src.PriorityId))
            //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(tic => tic.CategoryId)));
            //CreateMap<TaskUpdateDto, TaskItem>();

            //// Category Mapping
            //CreateMap<Category, CategoryGetDto>();
            //CreateMap<CategoryCreateDto, Category>();
            //CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
