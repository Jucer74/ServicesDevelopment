using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.Base;
using Pricat.Application.Common.Interfaces;
using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Persistence
{
    public class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
