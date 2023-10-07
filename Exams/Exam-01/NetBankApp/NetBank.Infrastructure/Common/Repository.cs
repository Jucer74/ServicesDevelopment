<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
﻿using Microsoft.EntityFrameworkCore;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
using NetBank.Domain.Common;
using NetBank.Domain.Exceptions;
using NetBank.Infrastructure.Context;
using System.Linq.Expressions;

namespace NetBank.Infrastructure.Common
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
<<<<<<< HEAD
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
=======
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync<T>();
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
<<<<<<< HEAD
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
=======
            return await _appDbContext.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);
            return entity!;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
        }

        public async Task RemoveAsync(T entity)
        {
<<<<<<< HEAD
            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var id = entity.Id;
=======
            var id = entity?.Id;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original is null)
            {
<<<<<<< HEAD
                var entityType = entity.GetType().Name;
                throw new NotFoundException($"{entityType} [{id}] Not Found");
            }

            _appDbContext.Entry(original).CurrentValues.SetValues(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entityList)
        {
            _appDbContext.Set<T>().RemoveRange(entityList);
            await _appDbContext.SaveChangesAsync();
=======
                throw new NotFoundException($"IssuingNetwork with Id={id} Not Found");
            }

            _appDbContext.Set<T>().Remove(entity!);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var id = entity?.Id;
            var original = await _appDbContext.Set<T>().FindAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"IssuingNetwork with Id={id} Not Found");
            }

            _appDbContext.Entry(original).CurrentValues.SetValues(entity!);
            await _appDbContext.SaveChangesAsync();

            return entity!;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
        }
    }
}