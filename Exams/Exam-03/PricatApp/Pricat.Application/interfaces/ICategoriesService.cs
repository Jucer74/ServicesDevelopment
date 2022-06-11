using Pricat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.interfaces
{
    public interface ICategoriesService
    {
        public Task Add(Categories entity);

        public Task<IEnumerable<Categories>> GetAll();

        public Task<Categories> GetById(int id);

        public Task<IEnumerable<Categories>> Find(Expression<Func<Categories, bool>> predicate);

        public Task Update(Categories entity);

        public Task Remove(int id);
    }
}
