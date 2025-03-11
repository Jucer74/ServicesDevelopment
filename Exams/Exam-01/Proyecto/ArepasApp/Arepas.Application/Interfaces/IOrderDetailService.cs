using Arepas.Domain.Dtos;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces;

public interface IOrderDetailService
{
    public Task<OrderDetail> AddAsync(OrderDetail entity);

    public Task<IEnumerable<OrderDetail>> FindAsync(Expression<Func<OrderDetail, bool>> predicate);

    public Task<IEnumerable<OrderDetail>> GetAllAsync();

    public Task<OrderDetail> GetByIdAsync(int id);

    public Task<ResponseData<OrderDetail>> GetByQueryParamsAsync(QueryParams queryParams);

    public Task RemoveAsync(int id);

    public Task<OrderDetail> UpdateAsync(int id, OrderDetail entity);
}