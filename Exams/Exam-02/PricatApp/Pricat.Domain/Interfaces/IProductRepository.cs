using Pricat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricat.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(int id, Product product);
        Task RemoveAsync(int id);
        Task<List<Product>> GetByCategoryIdAsync(int categoryId);
    }
}