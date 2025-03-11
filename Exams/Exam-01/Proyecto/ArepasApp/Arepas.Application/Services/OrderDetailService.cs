using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Services;

public class OrderDetailService : IOrderDetailService
{
    public Task<OrderDetail> AddAsync(OrderDetail entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetail>> FindAsync(Expression<Func<OrderDetail, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetail>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<OrderDetail>> GetByQueryParamsAsync(QueryParams queryParams)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetail> UpdateAsync(int id, OrderDetail entity)
    {
        throw new NotImplementedException();
    }
}