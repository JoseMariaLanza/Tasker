using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tasker.Helpers;
using Tasker.Services.DTO.TaskDTOs;
using Tasker.Services.Pagination;
using Tasker.Services.Tasks;

namespace Tasker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get filtered tasks.")]
        [SwaggerResponse(200, "Successfuly retrieved task list.", typeof(PaginationResult<TaskGetDto>))]
        [SwaggerResponse(204, "Does not exists any task yet.")]
        public async Task<IActionResult> GetTasks([FromQuery] SearchParams searchParams, [FromQuery] PaginationParams paginationParams)
        {
            var result = await _taskService.GetFilteredTasksAsync(searchParams, paginationParams);

            if (result.Count == 0) return ApiResponse.NoContent();

            return ApiResponse.Ok("Tasks retrieved successfuly.", "", result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new task.")]
        [SwaggerResponse(201, "Task successfuly created.", typeof(ApiResponse))]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto task)
        {
            var result = await _taskService.CreateTaskAsync(task);

            if (result == null) return ApiResponse.NoContent();

            return ApiResponse.Created("Task successfuly created.", "data", result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update an existing tastk.")]
        [SwaggerResponse(200, "Task successfuly updated.", typeof(ApiResponse))]
        public async Task<IActionResult> UpdateTask([FromBody] TaskUpdateDto task)
        {
            var result = await _taskService.UpdateTaskAsync(task);

            if (result is null) return ApiResponse.NoContent();

            return ApiResponse.Ok("Task successfuly updated", "data", result);
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Logic delete an existing task.")]
        [SwaggerResponse(200, "Task successfuly deleted.", typeof(ApiResponse))]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            var result = await _taskService.DeleteTaskAsync(taskId);

            if (result is null) return ApiResponse.NotFound("Task does not exists.");

            return ApiResponse.Ok("Task successfuly deleted", "data", result);
        }
    }
}
