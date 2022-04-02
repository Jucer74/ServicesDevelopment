using ReminderApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Reminderapp.Domain.common
{
    public interface IRepository<T> where T : EntityBase
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T Get8y1d(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Resove(T entity);
    }
}