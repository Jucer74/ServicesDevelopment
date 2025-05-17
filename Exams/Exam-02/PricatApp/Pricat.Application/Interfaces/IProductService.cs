using System.Linq.Expressions;
using Pricat.Domain.Entities;

namespace Pricat.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> AddAsync(Product entity);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);

        Task<Product> UpdateAsync(int id, Product entity);

        Task RemoveAsync(int id);
    }
}