using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Services
{
    // Define el contrato para la lógica de negocio relacionada con los productos.
    public interface IProductService
    {
        // Crea un nuevo producto.
        Task<Product> CreateAsync(Product product);

        // Elimina un producto por su ID.
        Task DeleteAsync(int id);

        // Obtiene todos los productos.
        Task<IEnumerable<Product>> GetAllAsync();

        // Obtiene un producto por su ID.
        Task<Product> GetByIdAsync(int id);

        // Obtiene productos por el ID de su categoría.
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);

        // Actualiza un producto existente por su ID.
        Task<Product> UpdateAsync(int id, Product product);

        // Obtiene productos por categoría (posiblemente con una lógica adicional distinta al método anterior).
        Task<List<Product>> GetProductsByCategory(int categoryId);
    }
}
