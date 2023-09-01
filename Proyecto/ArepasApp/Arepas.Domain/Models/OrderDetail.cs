using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arepas.Domain.Models;

public class OrderDetail : EntityBase
{
    [Required]
    [ForeignKey("FK_OrderDetails_Orders")]
    public int OrderId { get; set; }

    [Required]
    [ForeignKey("FK_OrderDetails_Products")]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal PriceOrd { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}