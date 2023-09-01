using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces;

public interface ICustomerService
{
    public Task<Customer> AddAsync(Customer entity);

    public Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate);

    public Task<IEnumerable<Customer>> GetAllAsync();

    public Task<Customer> GetByIdAsync(int id);

    public Task<ResponseData<Customer>> GetByQueryParamsAsync(QueryParams queryParams);

    public Task RemoveAsync(int id);

    public Task<Customer> UpdateAsync(int id, Customer entity);

    public Task<Customer> GetOrdersByCustomerId(int id);
}