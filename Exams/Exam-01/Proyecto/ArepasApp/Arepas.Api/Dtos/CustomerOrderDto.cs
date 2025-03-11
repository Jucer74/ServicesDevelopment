namespace Arepas.Api.Dtos
{
    public class CustomerOrderDto
    {
        public CustomerDto Customer { get; set; } = null!;
        public IEnumerable<OrderDto> Orders { get; set; } = null!;
    }
}