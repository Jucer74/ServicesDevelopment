using System.Collections.Generic;


namespace Pricat.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        // Propiedad de navegación
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
