using Microsoft.AspNetCore.Http;
using AutoMapper;
using Tasker.Repositories.Tasks;
using Tasker.Services.DTO;
using Microsoft.EntityFrameworkCore;
using Tasker.Services.Pagination;
using Tasker.Services.DTO.TaskDTOs;
using Tasker.Repositories.Tasks.Models;

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

            //foreach (var task in query)
            //{
            //    query = query.LoadSubTasksAsync(task);
            //}


            var tasks = await _taskRepository.GetFilteredTasksAsync(query, searchParams.Term, searchParams.Categories, paginationParams.Offset, paginationParams.Limit);

            foreach (var task in tasks)
            {
                await _taskRepository.LoadSubTasksRecursively(task);
            }

            var tasksGetDto = _mapper.Map<List<TaskGetDto>>(tasks);
            //var taskDtos = new List<TaskGetDto>();
            //foreach (var task in tasks)
            //{
            //    var taskDto = _mapper.Map<TaskGetDto>(task);
            //    taskDtos.Add(taskDto);
            //    // ... recursively load and map subtasks to DTOs, up to a certain depth
            //}


            int totalCount = await query.CountAsync();

            return new PaginationResult<TaskGetDto>()
            {
                Data = tasksGetDto,
                //Data = taskDtos,
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

            var newTask = await _taskRepository.CreateTaskAsync(taskModel, task.Categories);

            return _mapper.Map<TaskGetDto>(newTask);
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
