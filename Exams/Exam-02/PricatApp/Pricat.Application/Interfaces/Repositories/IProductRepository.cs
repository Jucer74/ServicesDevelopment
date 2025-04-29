using Pricat.Application.Common;
using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Repositories
{
    // Interfaz que extiende la funcionalidad del repositorio base para la entidad Product.
    // Define los métodos específicos para interactuar con la entidad Product en la base de datos.
    // Hereda de IRepository<Product>, lo que permite operaciones generales como AddAsync, GetByIdAsync, etc.
    public interface IProductRepository : IRepository<Product>
    {
        // Método que obtiene todos los productos de una categoría específica.
        // Este método es específico para la entidad Product, y no está en el repositorio base.
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    }
}
