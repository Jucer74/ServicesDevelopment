using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Services;

public class ProductService : IProductService
{
    public Task<Product> AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Product>> GetByQueryParamsAsync(QueryParams queryParams)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateAsync(int id, Product entity)
    {
        throw new NotImplementedException();
    }
}