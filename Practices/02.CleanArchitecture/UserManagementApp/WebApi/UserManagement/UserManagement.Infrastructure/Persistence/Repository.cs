using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Common;
using UserManagement.Infrastructure.Persistence.Context;

namespace UserManagement.Infrastructure.Persistence
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly UserDbContext _context;

        public Repository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            var id = entity?.Id;
            var original = await _context.Set<T>().FindAsync(id);

            if (original is null)
            {
            }

            _context.Set<T>().Remove(original);
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var id = entity?.Id;
            var original = await _context.Set<T>().FindAsync(id);
            if (original is null)
            {
            }
            _context.Entry(original).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}