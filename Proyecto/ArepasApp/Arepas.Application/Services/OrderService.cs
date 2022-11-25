using Arepas.Domain.Exceptions;
using Arepas.Application.Interfaces;
using Arepas.Domain.Interfaces.Repositories;
using System.Linq.Expressions;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Entities.Dto;

namespace Arepas.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> AddAsync(Order entity)
        {
            return await _orderRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _orderRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public Task<PaginationResult<Order>> GetByPageAsync(PaginationParams @params)
        {
            return _orderRepository.GetByPageAsync(@params);
        }

        public Task<IEnumerable<Order>> SearchAsync(string queryValue)
        {
            return _orderRepository.SearchAsync(queryValue);

        }

        public Task<PaginationResult<Order>> GetByCustomerAsync(int id)
        {
            var order = _orderRepository.GetByCustomerAsync(id);

            if (order == null)
            {
                throw new NotFoundException($"Order with Id={id} Not Found");
            }

            return order;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var product = await _orderRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Order with Id={id} Not Found");
            }

            return product;
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _orderRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Order with Id={id} Not Found");
            }

            await _orderRepository.RemoveAsync(product);

        }

        public async Task<Order> UpdateAsync(int id, Order entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var category = await _orderRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException($"Order with Id={id} Not Found");
            }
            return (await _orderRepository.UpdateAsync(entity));
        }
    }
}