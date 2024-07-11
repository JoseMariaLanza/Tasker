using Tasker.Services.DTO.TaskDTOs;
using Tasker.Services.Pagination;

namespace Tasker.Services.Tasks
{
    public interface ITaskService
    {
        Task<PaginationResult<TaskGetDto>> GetFilteredTasksAsync(SearchParams searchParams, PaginationParams paginationParams);
        
        Task<TaskGetDto> GetTaskAsync(Guid taskId);

        Task<TaskGetDto> CreateTaskAsync(TaskCreateDto task);

        Task<TaskGetDto> UpdateTaskAsync(TaskUpdateDto task);

        Task<TaskGetDto> DeleteTaskAsync(Guid taskId);

        //Task<TaskGetDto> UpdateTaskAsync(TaskUpdateDto task);
        //Task<TaskGetDto> DeleteTaskAsync(int taskId);
    }
}
