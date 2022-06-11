using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Entities;
using Pricat.Domain.interfaces.Repositories;
using Pricat.Infrastructure.Context;

namespace Pricat.Infrastructure.repositories
{
    public class ProductsRepository :Repository<Products>, IproductsRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<List<Products>> GetAllBycategoryId(int id)
        {
            return Task.FromResult(_appDbContext.Products.Where(x => x.CategoryId == id).ToList());
        }

    }
}
