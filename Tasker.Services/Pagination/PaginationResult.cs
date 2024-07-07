namespace Tasker.Services.Pagination
{
    public class PaginationResult<T>
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

        public int? Count { get; set; } = null;
    }
}
