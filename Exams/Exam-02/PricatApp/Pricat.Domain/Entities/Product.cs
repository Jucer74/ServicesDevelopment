using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Entities; // si es la ruta correcta


namespace Pricat.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string EanCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public decimal Price { get; set; }

        // Propiedad de navegación
        public Category Category { get; set; } = null!;
    }

}
