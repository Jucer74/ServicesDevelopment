using Arepas.Domain.Interfaces.Repositories;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Arepas.Domain.Exceptions;

namespace Arepas.Infrastructure.Repositories
{
    public class OrderDetailsRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderDetailsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<PaginationResult<OrderDetail>> GetByPageAsync(PaginationParams @params)
        {
            var orderDetailss = _appDbContext.OrderDetails.OrderBy(x => x.Id);

            var xTotalCount = orderDetailss.Count();

            var items = await orderDetailss.OrderBy(p => p.Id)
                .Skip((@params.Page - 1) * @params.Limit)
                .Take(@params.Limit)
                .ToListAsync<OrderDetail>();

            return new PaginationResult<OrderDetail>()
            {
                XTotalCount = xTotalCount,
                Links = @"<links>",
                Item = items
            };

        }
        public async Task<PaginationResult<OrderDetail>> GetByOrderAsync(int id)
        {
            var ordersDetails = _appDbContext.OrderDetails.OrderBy(x => x.OrderId);

            var xTotalCount = ordersDetails.Count();

            var items = await ordersDetails.OrderBy(p => p.OrderId)
                .Where(x => x.OrderId == id)
                .ToListAsync<OrderDetail>();

            return new PaginationResult<OrderDetail>()
            {
                XTotalCount = xTotalCount,
                Links = @"<links>",
                Item = items
            };

        }
        public async Task<OrderDetail> AddOrderDetail(OrderDetail orderDetails)
        {
            var idNew = orderDetails.OrderId;
            var idProduct = orderDetails.ProductId;
            var order = await _appDbContext.Set<Order>().FindAsync(idNew);
            var product = await _appDbContext.Set<Product>().FindAsync(idProduct);


            if (product is not null && order is not null)
            {
                var totalProduct = product.Price * orderDetails.Quantity;
                orderDetails.TotalProduct = totalProduct;
                _appDbContext.Set<OrderDetail>().Add(orderDetails);
                await _appDbContext.SaveChangesAsync();
                return orderDetails;

            }
            else
            {
                throw new NotFoundException($"La orden #{idNew} no existe o el producti {idProduct} no existe");


            }
        }
    }
}
