// Importación de los namespaces necesarios
using System.ComponentModel.DataAnnotations.Schema;  // Para el uso de [ForeignKey] y [Column]
using System.ComponentModel.DataAnnotations;  // Para validaciones de datos como [Required] y [StringLength]
using Pricat.Domain.Common;  // Para heredar de la clase base EntityBase

namespace Pricat.Domain.Models
{
    // Representa un producto, hereda de EntityBase
    public class Product : EntityBase
    {
        // Propiedad que representa la clave foránea a la categoría del producto
        // [ForeignKey("CategoryId")] establece una relación con la entidad Category
        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        // Propiedad que representa el código EAN (código de barras) del producto
        // [StringLength(13)] asegura que el código tiene una longitud de exactamente 13 caracteres
        [Required]
        [StringLength(13)]
        public string EanCode { get; set; } = null!;

        // Descripción del producto
        [Required]
        [StringLength(50)]
        public string Description { get; set; } = null!;

        // Unidad del producto (ej. "kg", "unidad", etc.)
        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = null!;

        // Precio del producto, definido como un decimal con hasta 13 dígitos y 2 decimales
        [Required]
        [Column(TypeName = "decimal(13,2)")]
        public decimal Price { get; set; }

        // Propiedad de navegación a la categoría a la que pertenece el producto
        // La propiedad virtual permite que esta relación sea cargada perezosamente (lazy loading)
        public virtual Category Category { get; set; } = null!;
    }
}
