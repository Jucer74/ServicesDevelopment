using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Services;

public interface IProductService
{
    Task<Product> CreateProduct(Product product);
    Task<List<Product>> GetAllProducts();

    Task<Product> GetProductById(int id);
    Task<List<Product>> GetProductsByCategoryId(int categoryId);

    Task<Product> UpdateProduct(int id, Product product);
    Task DeleteProduct(int id);

}