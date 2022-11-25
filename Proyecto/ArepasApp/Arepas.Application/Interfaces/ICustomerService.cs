using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces
{
    public interface ICustomerService
    {
        public Task<Customer> AddAsync(Customer entity);

        public Task<IEnumerable<Customer>> GetAllAsync();

        public Task<PaginationResult<Customer>> GetByPageAsync(PaginationParams @params);

        public Task<IEnumerable<Customer>> SearchAsync(string queryValue);

        public Task<Customer> GetByIdAsync(int id);

        public Task<IEnumerable<Customer>> LoginCustomer(LoginDto loginDto);

        public Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate);

        public Task<Customer> UpdateAsync(int id, Customer entity);

        public Task RemoveAsync(int id);
    }
}