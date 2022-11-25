namespace Arepas.Domain.Entities.Dto
{
    public class PaginationResult<T>
    {
        public int XTotalCount { get; set; }

        public string? Links { get; set; }

        public IEnumerable<T>? Item { get; set; }
    }
}
