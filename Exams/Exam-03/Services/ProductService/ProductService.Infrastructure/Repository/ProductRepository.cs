using ProductServiceAPI.Domain.Interfaces.Repositories;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Infrastructure.Common;
using ProductServiceAPI.Infrastructure.Context;

namespace ProductServiceAPI.Infrastructure.Repositories
{
   public class ProductRepository : Repository<Product>, IProductRepository
   {
      public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }
   }
}