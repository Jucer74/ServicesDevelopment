using Arepas.Domain.Models;

namespace Arepas.Application.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetCustomersAsync();
}