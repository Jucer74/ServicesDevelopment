using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pricat.Application.Common;
using Pricat.Domain.Common;
using Pricat.Infrastructure.Contex;

namespace Pricat.Infrastructure.Common
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            await _context.Set<T>().AddAsync(entity);
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
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with Id={id} not found");
            }
            return entity;
        }


        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }

            var id = entity?.Id;
            if (id == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity Id cannot be null");
            }

            var original = await _context.Set<T>().FindAsync(id);
            if (original == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }

            _context.Set<T>().Remove(original);
            await _context.SaveChangesAsync();
        }


        public async Task<T> UpdateAsync(int id, T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }

            var origin = await _context.Set<T>().FindAsync(id);
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity not found");
            }
            _context.Entry(origin).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
