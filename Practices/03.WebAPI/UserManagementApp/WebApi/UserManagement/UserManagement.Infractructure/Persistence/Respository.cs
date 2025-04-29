using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Common;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Common;
using UserManagement.Infractructure.Persistence.Context;


namespace UserManagement.Infractructure.Persistence
{
    public class Respository <T> : IRepository<T> where T : EntityBase
    {

        private readonly AppDbContext _appDbContext;

        public Respository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }
        public async Task RemoveAsync(T entity)
        {
            var id = entity?.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original is null)
            {
                throw new ArgumentNullException($"Person with Id={id} Not Found");
            }
            else
            {
                _appDbContext.Set<T>().Remove(original);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var id = entity?.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);
            if (original is null)
            {
                throw new ArgumentNullException($"Person with Id={id} Not Found");
            }
            else
            {
                _appDbContext.Entry(original).CurrentValues.SetValues(entity);
                await _appDbContext.SaveChangesAsync();
                return entity;
            }
        }
    }
}
