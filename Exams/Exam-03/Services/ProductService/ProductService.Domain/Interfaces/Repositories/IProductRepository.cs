using ProductService.Domain.Common;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}