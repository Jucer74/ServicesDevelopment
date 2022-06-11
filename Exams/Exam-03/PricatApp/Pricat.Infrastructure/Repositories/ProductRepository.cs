using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;
using System.Collections.Generic;

namespace Pricat.Infrastructure.Repositories
{
   public class ProductRepository : Repository<Product>, IProductRepository
   {
      public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }

      //public IEnumerable<Reminder> FindRemindersByCategory(Category category)
      //{
        // return (IEnumerable<Reminder>)base.FindAsync(c => c.Category.Equals(category));
      //}
      
      public IEnumerable<Product> FindReminderByCategoryId(int id)
        {
            return (IEnumerable<Product>)base.FindAsync(c => c.CategoryId.Equals(id));
        }
   }
}