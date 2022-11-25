using Arepas.Domain.Interfaces.Repositories;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;
using Arepas.Domain.Entities.Models;
using Arepas.Domain.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Arepas.Domain.Exceptions;

namespace Arepas.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaginationResult<Customer>> GetByPageAsync(PaginationParams @params)
        {
            var customers = _appDbContext.Customers.OrderBy(x => x.Id);

            var xTotalCount = customers.Count();

            var items = await customers.OrderBy(p => p.Id)
                .Skip((@params.Page - 1) * @params.Limit)
                .Take(@params.Limit)
                .ToListAsync<Customer>();

            return new PaginationResult<Customer>()
            {
                XTotalCount = xTotalCount,
                Links = @"<links>",
                Item = items
            };

        }

        public async Task<IEnumerable<Customer>> LoginCustomer(LoginDto loginDto)
        {
            var emailLogin = loginDto.Email;
            var PasswordLogin = loginDto.Password;
            //            var product = await _appDbContext.Set<Products>().FindAsync(idProduct);

            var customer = await _appDbContext.Customers.Where(x => x.Email.Equals(emailLogin) & x.Password.Equals(PasswordLogin)).ToListAsync();



            if (customer.Count != 0)
            {

                return await _appDbContext.Customers
                .Where(x => x.Email.Equals(emailLogin) & x.Password.Equals(PasswordLogin))
                .ToListAsync();


            }
            else
            {
                throw new NotFoundException($" EL usuario no existe");

            }
        }
        public async Task<IEnumerable<Customer>> SearchAsync(string queryValue)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var customers = await _appDbContext.Customers
                .Where(x => x.FirstName.Contains(queryValue)
                || x.LastName.Contains(queryValue)
                || x.Email.Contains(queryValue)
                || x.Address.Contains(queryValue)
                || x.PhoneNumber.Contains(queryValue))
                .ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return customers;
        }
    }
}