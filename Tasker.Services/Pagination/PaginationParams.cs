namespace Tasker.Services.Pagination;

public class PaginationParams
{
    public int Offset { get; set; } = 1; // Page number

    public int Limit { get; set; } = 25; // Page size

    public bool Count { get; set; } = true;
}
