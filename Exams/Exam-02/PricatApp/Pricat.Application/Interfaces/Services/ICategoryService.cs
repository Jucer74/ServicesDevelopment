using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Services
{
    // Define el contrato para la lógica de negocio relacionada con las categorías.
    public interface ICategoryService
    {
        // Crea una nueva categoría.
        Task<Category> CreateAsync(Category category);

        // Elimina una categoría por su ID.
        Task DeleteAsync(int id);

        // Obtiene todas las categorías.
        Task<IEnumerable<Category>> GetAllAsync();

        // Obtiene una categoría por su ID.
        Task<Category> GetByIdAsync(int id);

        // Actualiza una categoría existente por su ID.
        Task<Category> UpdateAsync(int id, Category category);
    }
}
