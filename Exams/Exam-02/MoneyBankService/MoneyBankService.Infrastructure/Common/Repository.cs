using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Common;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Infrastructure.Context;
using System.Linq.Expressions;

namespace MoneyBankService.Infrastructure.Common
{
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
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return (await _appDbContext.Set<T>().FindAsync(id))!;
        }

        public async Task RemoveAsync(T entity)
        {
            var id = entity?.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Item with Id={id} Not Found");
            }

            _appDbContext.Set<T>().Remove(entity!);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var id = entity?.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Item with Id={id} Not Found");
            }

            _appDbContext.Entry(original).CurrentValues.SetValues(entity!);
            await _appDbContext.SaveChangesAsync();

            return entity!;
        }
    }
}