using PricatApp.Domain.Entities;
using System.Linq.Expressions;

namespace PricatApp.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Product> AddAsync(Product entity);

        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<Product> GetByIdAsync(int id);

        public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);

        public Task<Product> UpdateAsync(int id, Product entity);

        public Task RemoveAsync(int id);
    }
}