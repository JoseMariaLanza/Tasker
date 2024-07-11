namespace Tasker.Services.Categories;

public class SearchParams
{
    public string? Term { get; set; }
    public List<Guid>? ParentCategories { get; set; }
}
