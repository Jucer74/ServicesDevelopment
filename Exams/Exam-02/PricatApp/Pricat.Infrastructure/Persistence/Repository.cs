using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces;
using Pricat.Domain.Common;
using Pricat.Infrastructure.Persistence.Context;

namespace Pricat.Infrastructure.Persistence
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public readonly PricatDbContext _context;

        public Repository(PricatDbContext context)
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
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity ?? throw new NotFoundException($"{typeof(T).Name} [{id}] Not Found");
        }

        public async Task RemoveAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"{typeof(T).Name} to delete is null.");
            }

            var original = await _context.Set<T>().FindAsync(entity.Id);

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