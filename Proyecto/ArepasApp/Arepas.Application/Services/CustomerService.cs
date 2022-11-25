using Arepas.Domain.Exceptions;
using Arepas.Application.Interfaces;
using Arepas.Domain.Interfaces.Repositories;

using System.Linq.Expressions;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Entities.Dto;

namespace Arepas.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            return await _customerRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _customerRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public Task<PaginationResult<Customer>> GetByPageAsync(PaginationParams @params)
        {
            return _customerRepository.GetByPageAsync(@params);
        }

        public Task<IEnumerable<Customer>> SearchAsync(string queryValue)
        {
            return _customerRepository.SearchAsync(queryValue);

        }


        public async Task<Customer> GetByIdAsync(int id)
        {
            var product = await _customerRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Customer with Id={id} Not Found");
            }

            return product;
        }

        public Task<IEnumerable<Customer>> LoginCustomer(LoginDto loginDto)
        {
            var product = _customerRepository.LoginCustomer(loginDto);

            if (product is null)
            {
                throw new NotFoundException($"Customer with  Not Found");
            }

            return product;
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _customerRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Customer with Id={id} Not Found");
            }

            await _customerRepository.RemoveAsync(product);
        }

        public async Task<Customer> UpdateAsync(int id, Customer entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var category = await _customerRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException($"Customer with Id={id} Not Found");
            }
            return (await _customerRepository.UpdateAsync(entity));
        }
    }
}
