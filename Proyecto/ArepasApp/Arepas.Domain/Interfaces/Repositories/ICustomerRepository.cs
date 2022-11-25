using Arepas.Domain.Common;
using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;

namespace Arepas.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<PaginationResult<Customer>> GetByPageAsync(PaginationParams @params);

        public Task<IEnumerable<Customer>> SearchAsync(string queryValue);

        public Task<IEnumerable<Customer>> LoginCustomer(LoginDto loginDto);


    }
}