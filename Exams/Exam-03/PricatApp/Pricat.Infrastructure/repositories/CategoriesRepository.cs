using Pricat.Domain.Entities;
using Pricat.Domain.interfaces.Repositories;
using Pricat.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Infrastructure.repositories
{
    public class CategoriesRepository : Repository<Categories>, ICategoriesRepository
    {

        public CategoriesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
