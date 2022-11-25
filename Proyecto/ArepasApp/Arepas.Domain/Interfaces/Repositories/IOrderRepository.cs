using Arepas.Domain.Common;
using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;

namespace Arepas.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<PaginationResult<Order>> GetByPageAsync(PaginationParams @params);

        public Task<IEnumerable<Order>> SearchAsync(string queryValue);
        public Task<PaginationResult<Order>> GetByCustomerAsync(int id);

    }
}