using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces.Repositories;
using ProductService.Infrastructure.Common;
using ProductService.Infrastructure.Context;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}