using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Exceptions;
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
            var entity = await _context.Set<T>().FindAsync(id);
            return entity ?? throw new NotFoundException($"{typeof(T).Name} [{id}] Not Found");
        }

        public async Task RemoveAsync(T entity)
        {
            var id = entity.Id;
            var original = await _context.Set<T>().FindAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"{typeof(T).Name} [{entity.Id}] Not Found");
            }

            _context.Set<T>().Remove(original);
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var id = entity.Id;
            var original = await _context.Set<T>().FindAsync(id);
            if (original is null)
            {
                throw new NotFoundException($"{typeof(T).Name} [{id}] Not Found");
            }
            _context.Entry(original).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}