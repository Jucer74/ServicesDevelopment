namespace Arepas.Api.Dtos
{
    public class OrderOrderDetailDto
    {
        public OrderDto Order { get; set; } = null!;
        public IEnumerable<OrderDetailProductDto> DetailProducts { get; set; } = null!;
    }
}
