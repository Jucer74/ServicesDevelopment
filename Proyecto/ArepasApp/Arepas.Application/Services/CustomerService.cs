using Arepas.Application.Interfaces;
using Arepas.Domain.Dtos;
using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Customer> AddAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Customer>> GetByQueryParamsAsync(QueryParams queryParams)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateAsync(int id, Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}