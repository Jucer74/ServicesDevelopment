namespace Arepas.Domain.Entities.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CustomerId { get; set; }

        public string? DeliveryFirstName { get; set; }

        public string? DeliveryLastName { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? DeliveryPhoneNumber { get; set; }

        public string? Notes { get; set; }

        public decimal? TotalOrder { get; set; }

    }
}
