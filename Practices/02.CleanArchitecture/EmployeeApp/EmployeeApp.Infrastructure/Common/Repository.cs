using EmployeeApp.Domain.Common;
using EmployeeApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeApp.Infrastructure.Common
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
         return await _appDbContext.Set<T>().FindAsync(id);
      }

      public async Task RemoveAsync(T entity)
      {
         _appDbContext.Set<T>().Remove(entity);
         await _appDbContext.SaveChangesAsync();
      }

      public async Task<T> UpdateAsync(T entity)
      {
         _appDbContext.Entry(entity).State = EntityState.Modified;
         await _appDbContext.SaveChangesAsync();
         return entity;
      }
    }
}