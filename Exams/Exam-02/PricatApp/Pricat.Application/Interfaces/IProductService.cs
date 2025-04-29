using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pricat.Domain.Entities;

namespace Pricat.Application.Interfaces
{
    public interface IProductService
    {
        Task<Products> AddAsync(Products entity);
        Task<IEnumerable<Products>> GetAllAsync();
        Task<Products> GetByIdAsync(int id);
        Task<IEnumerable<Products>> FindAsync(Expression<Func<Products, bool>> predicate);
        Task<Products> UpdateAsync(int id, Products entity);
        Task DeleteAsync(int id);
    }
}