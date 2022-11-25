using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces
{
    public interface IOrderDetailService
    {
        public Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail);

        public Task<IEnumerable<OrderDetail>> GetAllAsync();

        public Task<PaginationResult<OrderDetail>> GetByPageAsync(PaginationParams @params);

        public Task<PaginationResult<OrderDetail>> GetByOrderAsync(int id);

        public Task<OrderDetail> GetByIdAsync(int id);

        public Task<IEnumerable<OrderDetail>> FindAsync(Expression<Func<OrderDetail, bool>> predicate);

        public Task<OrderDetail> UpdateAsync(int id, OrderDetail entity);

        public Task RemoveAsync(int id);
    }
}