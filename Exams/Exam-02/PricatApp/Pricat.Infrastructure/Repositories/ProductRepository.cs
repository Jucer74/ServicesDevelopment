using Microsoft.EntityFrameworkCore;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Models;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;


namespace Pricat.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
           

        }
        
    }
}