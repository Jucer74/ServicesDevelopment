using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces;

public interface IProductService
{
    public Task<Product> AddAsync(Product entity);

    public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);

    public Task<IEnumerable<Product>> GetAllAsync();

    public Task<Product> GetByIdAsync(int id);

    public Task<ResponseData<Product>> GetByQueryParamsAsync(QueryParams queryParams);

    public Task RemoveAsync(int id);

    public Task<Product> UpdateAsync(int id, Product entity);
}