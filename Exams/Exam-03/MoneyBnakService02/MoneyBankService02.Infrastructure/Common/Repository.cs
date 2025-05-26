using Microsoft.EntityFrameworkCore;
using MoneyBankService02.Domain.Common;
using MoneyBankService02.Domain.Exceptions;
using MoneyBankService02.Infrastructure.Context;
using System;
using System.Linq.Expressions;

namespace MoneyBankService02.Infrastructure.Common;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _database;

    public Repository(AppDbContext context)
    {
        _database = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<T> AddAsync(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

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
