using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Common;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Infrastructure.Context;
using System.Linq.Expressions;

namespace MoneyBankService.Infrastructure.Common
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _database;

        public Repository(AppDbContext context)
        {
            _database = context;
        }

        public async Task<T> AddAsync(T item)
        {
            var dbSet = _database.Set<T>();
            await dbSet.AddAsync(item);
            await _database.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return await _database.Set<T>()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _database.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _database.Set<T>().FindAsync(id);
            return result ?? throw new NotFoundException($"Entidad con ID={id} no encontrada");
        }

        public async Task RemoveAsync(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var dbSet = _database.Set<T>();
            var existing = await dbSet.FindAsync(item.Id)
                ?? throw new NotFoundException($"Entidad con ID={item.Id} no existe");

            dbSet.Remove(existing);
            await _database.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var dbSet = _database.Set<T>();
            var existing = await dbSet.FindAsync(item.Id)
                ?? throw new NotFoundException($"Entidad con ID={item.Id} no existe");

            _database.Entry(existing).CurrentValues.SetValues(item);
            await _database.SaveChangesAsync();

            return item;
        }
    }
}