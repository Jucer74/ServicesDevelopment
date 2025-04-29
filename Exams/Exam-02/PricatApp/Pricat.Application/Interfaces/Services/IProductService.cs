
using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<Category> CreateProduct(Product product);

        Task DeleteProduct(int id);

        Task<List<Category>> GetAllProducts();

        Task<Category> GetProductById(int id);

        Task<Category> UpdateProduct(int id, Product product);
    }
}
