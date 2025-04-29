using UserManagement.Infrastr.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagement.Domain.Common;
using UserManagement.Domain.Exceptions;


namespace UserManagement.Infrastr.Common;


    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _appDbContext;

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
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _appDbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new NotFoundException($"Entity with id={id} not found");
        }
        return entity;
    }

    public async Task RemoveAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var id = entity.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original == null)
            {
                throw new NotFoundException($"User with Id={id} not found");
            }

            _appDbContext.Set<T>().Remove(original);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var id = entity.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original == null)
            {
                throw new NotFoundException($"User with Id={id} not found");
            }

            _appDbContext.Entry(original).CurrentValues.SetValues(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
    }


