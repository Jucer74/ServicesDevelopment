using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Models;

public class Product : EntityBase
{
    [Required(ErrorMessage = "El Nombre del Producto es Requerido")]
    [StringLength(100, ErrorMessage = "La Longitud Maxima del Nombre es de 100 Caracteres")]
    public string Name { get; set; } = null!;

    [StringLength(250, ErrorMessage = "La Longitud Maxima de la Descripcion es de 250 Caracteres")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "El Precio es Requerido")]
    public decimal Price { get; set; }

    [StringLength(250, ErrorMessage ="El Nombre de la Imagen es de Maximo 250 caracteres")]
    public string? Image { get; set; }

    public virtual ICollection<OrderDetail> Orderdetails { get; set; } = new List<OrderDetail>();
}