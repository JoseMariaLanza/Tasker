using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tasker.Helpers;
using Tasker.Services.DTO;
using Tasker.Services.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tasker.Services.Pagination;
using Tasker.Services.DTO.TaskDTOs;

namespace Tasker.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: TaskController
        [HttpGet]
        [SwaggerOperation(Summary = "Get filtered tasks.")]
        [SwaggerResponse(200, "Successfuly retrieved task list.", typeof(PaginationResult<TaskGetDto>))]
        [SwaggerResponse(204, "Does not exists any task yet.")]
        public async Task<IActionResult> GetTasks([FromQuery] SearchParams searchParams, [FromQuery] PaginationParams paginationParams)
        {
            var result = await _taskService.GetFilteredTasksAsync(searchParams, paginationParams);

            if (result.Count == 0) return ApiResponse.NoContent();

            return ApiResponse.Ok("Successfully retrieved tasks.", "data", result);
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
    }
}
