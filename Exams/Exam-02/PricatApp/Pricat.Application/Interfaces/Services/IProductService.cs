using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Services;

public interface IProductService
{
    Task<Product> CreateAsync(Product product);
    Task DeleteAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    Task<Product> UpdateAsync(int id, Product product);
    Task<List<Product>> GetProductsByCategory(int categoryId);
}
