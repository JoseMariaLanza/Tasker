using Tasker.Services.DTO.TaskDTOs;

public class TaskPaginatedResult
{
    public List<TaskGetDto> Tasks { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
