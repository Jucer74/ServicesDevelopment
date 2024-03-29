﻿using Pricat.Domain.Interfaces.Repositories;
using Pricat.Domain.Entities;
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