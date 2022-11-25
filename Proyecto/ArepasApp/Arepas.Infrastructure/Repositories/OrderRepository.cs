using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Arepas.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<PaginationResult<Order>> GetByPageAsync(PaginationParams @params)
        {
            var orders = _appDbContext.Orders.OrderBy(x => x.Id);

            var xTotalCount = orders.Count();

            var items = await orders.OrderBy(p => p.Id)
                .Skip((@params.Page - 1) * @params.Limit)
                .Take(@params.Limit)
                .ToListAsync<Order>();

            return new PaginationResult<Order>()
            {
                XTotalCount = xTotalCount,
                Links = @"<links>",
                Item = items
            };

        }

        public async Task<PaginationResult<Order>> GetByCustomerAsync(int id)
        {
            var orders = _appDbContext.Orders.OrderBy(x => x.CustomerId);

            var xTotalCount = orders.Count();

            var items = await orders.OrderBy(p => p.CustomerId)
                .Where(x => x.CustomerId == id)
                .ToListAsync<Order>();

            return new PaginationResult<Order>()
            {
                XTotalCount = xTotalCount,
                Links = @"<links>",
                Item = items
            };

        }

        public async Task<IEnumerable<Order>> SearchAsync(string queryValue)
        {
            var orders = await _appDbContext.Orders
                .Where(x => x.DeliveryFirstName.Contains(queryValue)
                || x.DeliveryAddress.Contains(queryValue)
                || x.DeliveryLastName.Contains(queryValue)
                || x.DeliveryPhoneNumber.Contains(queryValue)
                || x.Notes.Contains(queryValue)
                )
                .ToListAsync();
            return orders;
        }
    }
}
