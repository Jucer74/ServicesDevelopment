using ProductServiceAPI.Domain.Common;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Domain.Interfaces.Repositories
{
   public interface IProductRepository : IRepository<Product>
   {
   }
}