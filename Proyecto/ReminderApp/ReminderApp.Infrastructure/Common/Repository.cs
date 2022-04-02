
using Microsoft.EntityFrameworkCore;
using Reminderapp.Domain.common;
using ReminderApp.Domain.Common;
using ReminderApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Infrastructure.common
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbcontext _appDbcontext;
        public Repository(AppDbcontext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        public void Add(T entity)
        {
            _appDbcontext.Set<T>().Add(entity);
            _appDbcontext.SaveChanges();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _appDbcontext.Set<T>().Where(predicate).AsEnumerable();
        }

        public T Get8y1d(int id)
        {
            return _appDbcontext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _appDbcontext.Set<T>().AsEnumerable();
        }

        public void Resove(T entity)
        {
            _appDbcontext.Set<T>().Remove(entity);
            _appDbcontext.SaveChanges();
        }

        public void Update(T entity)
        {
            _appDbcontext.Entry(entity).State = EntityState.Modified;
            _appDbcontext.SaveChanges();
        }
    }
}
