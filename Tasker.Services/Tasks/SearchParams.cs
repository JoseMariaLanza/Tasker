namespace Tasker.Services.Tasks;

public class SearchParams
{
    public string? Term { get; set; }
    public List<Guid>? Categories { get; set; } = null;

    public Guid? ParentTaskId { get; set; } = null;

    public bool getSubTasks { get; set; } = false;
}
