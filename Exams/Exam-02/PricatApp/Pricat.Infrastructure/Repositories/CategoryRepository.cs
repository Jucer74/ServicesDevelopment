using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;
using Pricat.Infrastructure.Persistence;
using Pricat.Infrastructure.Persistence.Context;

namespace Pricat.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(PricatDbContext _context) : base(_context)
        {
        }
}
}