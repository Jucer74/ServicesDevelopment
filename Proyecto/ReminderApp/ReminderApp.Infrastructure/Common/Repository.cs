using Microsoft.EntityFrameworkCore;
using ReminderApp.Domain.Common;
using ReminderApp.Infraestucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Infrastructure.Common
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDBContext _appDbContext;

        public Repository(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext; 
        }
        public void Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            _appDbCOntext.SaveChanges();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetById(int id)
        {
            throw new NotImplementedException();
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
