using System.Linq.Expressions;
using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);

        Task DeleteProductr(int id);

        Task<List<Product>> GetAllProducts();

        Task<Product> GetProductById(int id);

        Task<Product> UpdateProduct(int id, Product product);
        Task<List<Product>> GetProductsByCategory(int categoryId);
    }
}
