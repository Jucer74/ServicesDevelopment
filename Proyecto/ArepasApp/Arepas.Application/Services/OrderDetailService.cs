using Arepas.Application.Interfaces;
using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Exceptions;
using Arepas.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Arepas.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail)
        {
            return await _orderDetailRepository.AddOrderDetail(orderDetail);
        }

        public async Task<IEnumerable<OrderDetail>> FindAsync(Expression<Func<OrderDetail, bool>> predicate)
        {
            return await _orderDetailRepository.FindAsync(predicate);
        }
        public async Task<PaginationResult<OrderDetail>> GetByOrderAsync(int id)
        {
            return await _orderDetailRepository.GetByOrderAsync(id);
        }
        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await _orderDetailRepository.GetAllAsync();
        }

        public Task<PaginationResult<OrderDetail>> GetByPageAsync(PaginationParams @params)
        {
            return _orderDetailRepository.GetByPageAsync(@params);
        }

        public async Task<OrderDetail> GetByIdAsync(int id)
        {
            var product = await _orderDetailRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"OrderDetail with Id={id} Not Found");
            }

            return product;
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _orderDetailRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"OrderDetail with Id={id} Not Found");
            }

            await _orderDetailRepository.RemoveAsync(product);
        }

        public async Task<OrderDetail> UpdateAsync(int id, OrderDetail entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var category = await _orderDetailRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException($"OrderDetails with Id={id} Not Found");
            }
            return (await _orderDetailRepository.UpdateAsync(entity));
        }
    }
}