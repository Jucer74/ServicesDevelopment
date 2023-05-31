using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces;

public interface IOrderService
{
    public Task<Order> AddAsync(Order entity);

    public Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate);

    public Task<IEnumerable<Order>> GetAllAsync();

    public Task<Order> GetByIdAsync(int id);

    public Task<ResponseData<Order>> GetByQueryParamsAsync(QueryParams queryParams);

    public Task RemoveAsync(int id);

    public Task<Order> UpdateAsync(int id, Order entity);
}