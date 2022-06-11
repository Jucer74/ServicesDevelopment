using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;

namespace Pricat.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
