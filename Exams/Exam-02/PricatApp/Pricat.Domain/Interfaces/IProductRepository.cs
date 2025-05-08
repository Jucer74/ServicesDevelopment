using Pricat.Domain.Entities;

namespace Pricat.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);

        Task<Product> AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task<bool> DeleteAsync(Product product);

        Task<bool> IsEanCodeUniqueAsync(string eanCode, int? excludedId = null);
    }
}