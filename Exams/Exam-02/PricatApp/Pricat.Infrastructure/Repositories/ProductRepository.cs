using PricatApp.Domain.Entities;
using PricatApp.Domain.Interfaces.Repositories;
using PricatApp.Infrastructure.Common;
using PricatApp.Infrastructure.Context;

namespace PricatApp.Infrastructure.Repositories
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}