using Arepas.Application.Interfaces;
using Arepas.Domain.Models;

namespace Arepas.Application.Services
{
    public class CustomerService : ICustomerService
    {
        public Task<List<Customer>> GetCustomersAsync()
        {
            throw new NotImplementedException();
        }
    }
}