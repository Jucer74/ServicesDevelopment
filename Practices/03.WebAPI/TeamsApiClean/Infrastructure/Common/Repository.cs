using System.Linq.Expressions;
using Application.Common;
using Application.Exceptions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;


namespace Infrastructure.Common
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            var id = entity.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            // this is weird
            if (original is null)
            {
                throw new NotFoundException($"User with Id={id} Not Found");
            }

            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var id = entity.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"User with Id={id} Not Found");
            }

            _appDbContext.Entry(original).CurrentValues.SetValues(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
