using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> AddAsync(Order entity);

        public Task<IEnumerable<Order>> GetAllAsync();

        public Task<PaginationResult<Order>> GetByPageAsync(PaginationParams @params);

        public Task<IEnumerable<Order>> SearchAsync(string queryValue);

        public Task<PaginationResult<Order>> GetByCustomerAsync(int id);

        public Task<Order> GetByIdAsync(int id);

        public Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate);

        public Task<Order> UpdateAsync(int id, Order entity);

        public Task RemoveAsync(int id);
    }
}