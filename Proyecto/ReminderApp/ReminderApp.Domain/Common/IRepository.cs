using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReminderApp.Domain.Common
{
   public interface IRepository<T> where T : EntityBase
   {
      void Add(T entity);

      IEnumerable<T> GetAll();

      T GetById(int id);

      IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

      void Update(T entity);

      void Remove(T entity);
   }
}