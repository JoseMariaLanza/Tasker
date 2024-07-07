using Microsoft.AspNetCore.Http;
using AutoMapper;
using Tasker.Repositories.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasker.Services.Pagination;
using Tasker.Services.DTO.TaskDTOs;
using Tasker.Repositories.Tasks.Models;
using Tasker.Repositories.Categories.Models;
using Tasker.Services.DTO.CategoryDTOs;
using System.Threading.Tasks;

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
            var query = _taskRepository.Query();

            if (searchParams.Term is not null)
            {
                query = query.FilterByNameOrDescription(searchParams.Term);
            }
            if (searchParams.Categories is not null)
            {
                query = query.FilterByCategories(searchParams.Categories);
            }


            var tasks = await _taskRepository.GetFilteredTasksAsync(query, searchParams.Term, searchParams.Categories, paginationParams.Offset, paginationParams.Limit);


            List<Category>? relatedCategories = null;
            foreach (var task in tasks)
            {
                await _taskRepository.LoadSubTasksRecursively(task);
                //List<int> categorySearchList = searchParams.Categories?.Count() > 0 ?
                //    searchParams.Categories :
                //    task.TaskItemCategories.Select(x => x.CategoryId).ToList();

                //relatedCategories = await _taskRepository.LoadSubTasksCategoriesAsync(task, null);
            }

            var tasksGetDto = _mapper.Map<List<TaskGetDto>>(tasks);

            foreach (var taskGetDto in tasksGetDto)
            {
                relatedCategories = await _taskRepository.LoadTasksCategoriesAsync(taskGetDto.Id, null);

                if (relatedCategories is not null)
                {
                    var categoriesGetDto = _mapper.Map<List<CategoryGetDto>>(relatedCategories);

                    taskGetDto.Categories = categoriesGetDto;
                    //foreach (var taskGetDto in tasksGetDto)
                    //{
                    //}
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

        public async Task<TaskGetDto> GetTaskAsync(int taskId)
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

            return _mapper.Map<TaskGetDto>(newTask);
        }

        public async Task<TaskGetDto> UpdateTaskAsync(TaskUpdateDto task)
        {

            var depth = await CalculateDepth(task.ParentTaskId);
            if (depth >= 64)
            {
                throw new InvalidOperationException("The task hierarchy exceeds the maximum allowed depth.");
            }

            //var taskItemCategories = task.CategoryIds.Distinct().Select(categoryId => new TaskItemCategory
            //{
            //    TaskItemId = task.Id,
            //    CategoryId = categoryId
            //}).ToList();
            

            var taskModel = _mapper.Map<TaskItem>(task);
            //taskModel.Categories = taskItemCategories;

            var updatedTask = await _taskRepository.UpdateTaskAsync(taskModel, taskModel.TaskItemCategories);

            var taskItem = _mapper.Map<TaskGetDto>(updatedTask);

            //return _mapper.Map<TaskGetDto>(newTask);
            return taskItem;
        }

        public async Task<TaskGetDto> DeleteTaskAsync(int taskId)
        {
            return _mapper.Map<TaskGetDto>(await _taskRepository.DeleteTaskAsync(taskId));
        }

        private async Task<int> CalculateDepth(int? parentTaskId, int currentDepth = 0)
        {
            if (!parentTaskId.HasValue)
            {
                return currentDepth;
            }

            var parentTask = await _taskRepository.GetTaskByIdAsync(parentTaskId.Value);
            return await CalculateDepth(parentTask.ParentTaskId, currentDepth + 1);
        }



        //public async Task<TaskGetDto> UpdateTaskAsync(TaskUpdateDto task)
        //{
        //}

        //public async Task<TaskGetDto> DeleteTaskAsync(int taskId)
        //{
        //}

    }
}
