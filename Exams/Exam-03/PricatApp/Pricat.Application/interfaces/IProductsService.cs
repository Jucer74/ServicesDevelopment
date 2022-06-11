using Pricat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.interfaces
{
    public interface IProductsService
    {
        public Task Add(Products entity);

        public Task<IEnumerable<Products>> GetAll();

        public Task<Products> GetById(int id);

        public Task<IEnumerable<Products>> Find(Expression<Func<Products, bool>> predicate);

        public Task Update(Products entity);

        public Task Remove(int id);
        public Task<IEnumerable<Products>> GetAllBycategoryId(int id);
    }
}
