using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tasker.Repositories.Categories.Models;
using Tasker.Repositories.Tasks;
using Tasker.Repositories.Tasks.Models;
using Tasker.Services.DTO.CategoryDTOs;
using Tasker.Services.DTO.TaskDTOs;
using Tasker.Services.Pagination;

namespace Tasker.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<TaskGetDto>> GetFilteredTasksAsync(SearchParams searchParams, PaginationParams paginationParams)
        {
            var query = searchParams.getSubTasks ? _taskRepository.Query(searchParams.ParentTaskId) : _taskRepository.Query();

            if (searchParams.Term is not null)
            {
                query = query.FilterByNameOrDescription(searchParams.Term);
            }
            if (searchParams.Categories is not null)
            {
                query = query.FilterByCategories(searchParams.Categories);
            }

            var tasks = await _taskRepository.GetFilteredTasksAsync(query, searchParams.Term, searchParams.Categories, paginationParams.Offset, paginationParams.Limit);

            List<Category>? relatedCategories;

            foreach (var task in tasks)
            {
                await _taskRepository.LoadSubTasksRecursively(task);
            }

            var tasksGetDto = _mapper.Map<List<TaskGetDto>>(tasks);

            foreach (var taskGetDto in tasksGetDto)
            {
                relatedCategories = await _taskRepository.LoadTasksCategoriesAsync(taskGetDto.Id, null);

                if (relatedCategories is not null)
                {
                    var categoriesGetDto = _mapper.Map<List<CategoryGetDto>>(relatedCategories);

                    taskGetDto.Categories = categoriesGetDto;
                }
            }

            int totalCount = await query.CountAsync();

            return new PaginationResult<TaskGetDto>()
            {
                Data = tasksGetDto,
                Offset = paginationParams.Offset,
                Limit = paginationParams.Limit,
                Count = totalCount,
            };
        }

        public async Task<TaskGetDto> GetTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);

            if (task is null)
            {
                throw new BadHttpRequestException("Task doesn't exists.");
            }

            return _mapper.Map<TaskGetDto>(task);
        }

        public async Task<TaskGetDto> CreateTaskAsync(TaskCreateDto task)
        {

            var depth = await CalculateDepth(task.ParentTaskId);
            if (depth >= 64)
            {
                throw new InvalidOperationException("The task hierarchy exceeds the maximum allowed depth.");
            }

            var taskModel = _mapper.Map<TaskItem>(task);

            var newTask = await _taskRepository.CreateTaskAsync(taskModel, task.CategoryIds);

            var taskGetDto = _mapper.Map<TaskGetDto>(newTask);
            List<Category>? relatedCategories;

            relatedCategories = await _taskRepository.LoadTasksCategoriesAsync(taskGetDto.Id, null);

            if (relatedCategories is not null)
            {
                var categoriesGetDto = _mapper.Map<List<CategoryGetDto>>(relatedCategories);

                taskGetDto.Categories = categoriesGetDto;
            }

            return taskGetDto;
        }

        public async Task<TaskGetDto> UpdateTaskAsync(TaskUpdateDto task)
        {
            var depth = await CalculateDepth(task.ParentTaskId);
            if (depth >= 64)
            {
                throw new InvalidOperationException("The task hierarchy exceeds the maximum allowed depth.");
            }
            
            var taskModel = _mapper.Map<TaskItem>(task);

            var taskItemList = taskModel.TaskItemCategories.ToList();

            var updatedTask = await _taskRepository.UpdateTaskAsync(taskModel, taskItemList);

            var taskGetDto = _mapper.Map<TaskGetDto>(updatedTask);
            List<Category>? relatedCategories;

            relatedCategories = await _taskRepository.LoadTasksCategoriesAsync(taskGetDto.Id, null);

            if (relatedCategories is not null)
            {
                var categoriesGetDto = _mapper.Map<List<CategoryGetDto>>(relatedCategories);

                taskGetDto.Categories = categoriesGetDto;
            }

            return taskGetDto;
        }

        public async Task<TaskGetDto> DeleteTaskAsync(Guid taskId)
        {
            return _mapper.Map<TaskGetDto>(await _taskRepository.DeleteTaskAsync(taskId));
        }

        private async Task<int> CalculateDepth(Guid? parentTaskId, int currentDepth = 0)
        {
            if (!parentTaskId.HasValue)
            {
                return currentDepth;
            }

            var parentTask = await _taskRepository.GetTaskByIdAsync(parentTaskId.Value);
            return await CalculateDepth(parentTask.ParentTaskId, currentDepth + 1);
        }
    }
}
