using System.Linq.Expressions;
using Pricat.Domain.Entities;

namespace Pricat.Application.Interfaces;

public interface IProductService
{
    public Task<Product> AddAsync(Product entity);
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate); 
    public Task<Product> UpdateAsync(int id, Product entity);
    public Task RemoveAsync(int id);
}