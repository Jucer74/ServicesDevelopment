using Product.Domain.Interfaces.Repositories;
using Product.Domain.Entities;
using Product.Infrastructure.Common;
using Product.Infrastructure.Context;

namespace Product.Infrastructure.Repositories;

public class ProductRepository : Repository<EProduct>, IProductRepository
{
    public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}