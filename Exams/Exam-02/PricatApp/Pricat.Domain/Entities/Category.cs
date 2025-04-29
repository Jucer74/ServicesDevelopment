using Pricat.Domain.Exceptions;

namespace Pricat.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!; // Cambiado de Name a Description para coincidir con DB
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public void ValidateDelete()
        {
            if (Products.Any())
            {
                throw new DomainException(
                    "CATEGORY_HAS_PRODUCTS",
                    "No se puede eliminar una categoría con productos asociados"
                );
            }
        }
    }
}