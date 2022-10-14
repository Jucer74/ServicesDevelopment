using PricatApp.Domain.Entities;
using PricatApp.Domain.Interfaces.Repositories;
using PricatApp.Infrastructure.Common;
using PricatApp.Infrastructure.Context;

namespace PricatApp.Infrastructure.Repositories
{
    public class CategorieRepository : Repository<Categorie>, ICategorieRepository
    {
        public CategorieRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}