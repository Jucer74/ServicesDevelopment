using PricatApp.Domain.Entities;
using System.Linq.Expressions;

namespace PricatApp.Application.Interfaces
{
    public interface ICategorieService
    {
        public Task<Categorie> AddAsync(Categorie entity);

        public Task<IEnumerable<Categorie>> GetAllAsync();

        public Task<Categorie> GetByIdAsync(int id);

        public Task<IEnumerable<Categorie>> FindAsync(Expression<Func<Categorie, bool>> predicate);

        public Task<Categorie> UpdateAsync(int id, Categorie entity);

        public Task RemoveAsync(int id);
    }
}