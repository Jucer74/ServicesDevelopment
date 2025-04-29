using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pricat.Application.Common;
using Pricat.Application.Exceptions;
using Pricat.Domain.Common;
using Pricat.Infrastructure.Context;

namespace Pricat.Infrastructure.Common;

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
        if (!int.TryParse(id.ToString(), out var numericId) || numericId <= 0)
        {
            throw new BadRequestException($"The value '{numericId}' is not valid.");
        }
        var entity = await _appDbContext.Set<T>().FindAsync(id);

        // Validar si la entidad no existe
        if (entity == null)
        {
            throw new NotFoundException($"{typeof(T).Name} [{id}] Not Found");
        }

        return entity;
        
        //return await _appDbContext.Set<T>().FindAsync(id);
    }
    
    public async Task<T> UpdateAsync(T entity)
    {
        var id = entity?.Id;
        var original = await _appDbContext.Set<T>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"{typeof(T).Name} [{id}] Not Found");
        }

        _appDbContext.Entry(original).CurrentValues.SetValues(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }
    
    public async Task RemoveAsync(T entity)
    {
        var id = entity?.Id;
        var original = await _appDbContext.Set<T>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"{typeof(T).Name} [{id}] Not Found");
        }

        if (entity != null) _appDbContext.Set<T>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }
}