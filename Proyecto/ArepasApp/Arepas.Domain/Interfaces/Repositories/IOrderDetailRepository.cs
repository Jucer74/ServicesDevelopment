using Arepas.Domain.Common;
using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;

namespace Arepas.Domain.Interfaces.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        public Task<PaginationResult<OrderDetail>> GetByPageAsync(PaginationParams @params);

        public Task<OrderDetail> AddOrderDetail(OrderDetail orderDetails);

        public Task<PaginationResult<OrderDetail>> GetByOrderAsync(int id);

    }
}