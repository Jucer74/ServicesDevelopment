using Arepas.Domain.Common;

namespace Arepas.Domain.Dtos;

public class ResponseData<T> where T : EntityBase
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public PaginationData XPagination { get; set; } = null!;
}