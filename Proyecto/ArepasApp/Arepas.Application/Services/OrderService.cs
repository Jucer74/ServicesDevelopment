using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Services;

public class OrderService : IOrderService
{
    public Task<Order> AddAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Order>> GetByQueryParamsAsync(QueryParams queryParams)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateAsync(int id, Order entity)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderDetailsByOrderId(int id)
    {
        throw new NotImplementedException();
    }

}