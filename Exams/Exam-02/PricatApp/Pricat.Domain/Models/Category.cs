// Importación de los namespaces necesarios
using System.ComponentModel.DataAnnotations;  // Para validaciones de datos como [Required] y [StringLength]
using Pricat.Domain.Common;  // Para heredar de la clase base EntityBase

namespace Pricat.Domain.Models
{
    // Representa una categoría de productos, hereda de EntityBase
    public class Category : EntityBase
    {
        // Propiedad de descripción de la categoría con validación
        // [Required] asegura que esta propiedad debe tener un valor
        // [StringLength(50)] restringe la longitud máxima a 50 caracteres
        [Required]
        [StringLength(50)]
        public string Description { get; set; } = null!;

        // Lista de productos asociados a esta categoría
        // La propiedad virtual indica que esta colección puede ser cargada de manera perezosa (lazy loading)
        public virtual List<Product> Products { get; set; } = null!;
    }
}
