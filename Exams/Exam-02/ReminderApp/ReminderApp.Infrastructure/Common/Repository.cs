﻿using Microsoft.EntityFrameworkCore;
using ReminderApp.Domain.Common;
using ReminderApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReminderApp.Infrastructure.Common
{
    public class Repository<T> : IRepository<T> where T : EntityBase
   {
      private readonly AppDbContext _appDbContext;


        public Repository(AppDbContext appDbContext)
      {
         _appDbContext = appDbContext;
      }

      public async Task Add(T entity)
      {
         _appDbContext.Set<T>().Add(entity);
         await _appDbContext.SaveChangesAsync();
      }

      public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
      {
         return await _appDbContext.Set<T>().Where(predicate).ToListAsync<T>();
      }


      public async Task<IEnumerable<T>> GetAll()
      {
         return await _appDbContext.Set<T>().ToListAsync<T>();
      }

      public async Task<T> GetById(int id)
      {
         return await _appDbContext.Set<T>().FindAsync(id);
      }

      public async Task Remove(T entity)
      {
         _appDbContext.Set<T>().Remove(entity);
         await _appDbContext.SaveChangesAsync();
      }

      public async Task Update(T entity)
      {
         _appDbContext.Entry(entity).State = EntityState.Modified;
         await _appDbContext.SaveChangesAsync();
      }
      
    }
}