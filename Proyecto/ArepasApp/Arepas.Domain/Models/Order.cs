using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arepas.Domain.Models;

public class Order : EntityBase
{
    [Required]
    [ForeignKey("FK_Orders_Customers")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "El Nombre Completo para la Entrega es Requerido")]
    [StringLength(100, ErrorMessage = "La Longitud Maxima para Nombre Completo para la Entrega es de 100 Caracteres")]
    public string DeliveryFullName { get; set; } = null!;

    [Required(ErrorMessage = "La Direccion para la Entrega es Requerida")]
    [StringLength(250, ErrorMessage = "La Longitud Maxima para la Direccion de Entrega es de 250 Caracteres")]
    public string DeliveryAddress { get; set; } = null!;

    [Required(ErrorMessage = "El Telefono para le Entrega es Requerido")]
    [StringLength(50, ErrorMessage = "La Longitud Maxima para el Telefono de Entrega es de 50 Caracteres")]
    public string DeliveryPhoneNumber { get; set; } = null!;

    [Required]
    public decimal TotalPrice { get; set; }

    [StringLength(250, ErrorMessage = "La Longitud Maxima para las Notas es de 250 Caracteres")]
    public string? Notes { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> Orderdetails { get; set; } = new List<OrderDetail>();
}