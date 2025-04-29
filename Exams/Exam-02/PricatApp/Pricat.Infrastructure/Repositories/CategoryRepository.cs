using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Contex;

namespace Pricat.Infrastructure.Repositoies
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}