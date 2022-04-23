using Microsoft.EntityFrameworkCore;
using ReminderApp.Domain.Common;
using ReminderApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReminderApp.Infrastructure.Common
{
   public class Repository<T> : IRepository<T> where T : EntityBase
   {
      private readonly AppDbContext _appDbContext;

      public Repository(AppDbContext appDbContext)
      {
         _appDbContext = appDbContext;
      }

      public void Add(T entity)
      {
         _appDbContext.Set<T>().Add(entity);
         _appDbContext.SaveChanges();
      }

      public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
      {
         return _appDbContext.Set<T>().Where(predicate).AsEnumerable();
      }

      public IEnumerable<T> GetAll()
      {
         return _appDbContext.Set<T>().AsEnumerable();
      }

      public T GetById(int id)
      {
         return _appDbContext.Set<T>().Find(id);
      }

      public void Remove(T entity)
      {
         _appDbContext.Set<T>().Remove(entity);
         _appDbContext.SaveChanges();
      }

      public void Update(T entity)
      {
         _appDbContext.Entry(entity).State = EntityState.Modified;
         _appDbContext.SaveChanges();
      }
   }
}