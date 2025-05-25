using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Common;
using MoneyBankService.Infrastructure.Context;
using System.Linq.Expressions;

namespace MoneyBankService.Infrastructure.Common;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();

    public async Task<T> UpdateAsync(T entity)
    {
        var existing = await _context.Set<T>().FindAsync(entity.Id);
        if (existing == null) throw new KeyNotFoundException("Entidad no encontrada");

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task RemoveAsync(T entity)
    {
        var existing = await _context.Set<T>().FindAsync(entity.Id);
        if (existing == null) throw new KeyNotFoundException("Entidad no encontrada");

        _context.Set<T>().Remove(existing);
        await _context.SaveChangesAsync();
    }
}