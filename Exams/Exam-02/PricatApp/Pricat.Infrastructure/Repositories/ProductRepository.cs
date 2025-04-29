using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Contex;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pricat.Infrastructure.Context;
using Pricat.Domain.Entities; 
using Pricat.Application.Interfaces; 


namespace Pricat.Infrastructure.Repositoies
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        private readonly PricatAppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}