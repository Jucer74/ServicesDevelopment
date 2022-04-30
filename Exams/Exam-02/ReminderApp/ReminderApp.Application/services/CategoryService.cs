using ReminderApp.Application.exception;
using ReminderApp.Application.Interfaces;
using ReminderApp.Domain.Common;
using ReminderApp.Domain.Entities;
using ReminderApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Add(Category entity)
        {
            await _categoryRepository.Add(entity);
        }

        public async Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate)
        {
            return await _categoryRepository.Find(predicate);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            var var = await _categoryRepository.GetById(id);

            if (var != null)
            {
                return var;
            }
            else
            {
                throw new Exceptions("Id invalido");
            }
        }

        public async Task Remove(int id)
        {
            var var = await _categoryRepository.GetById(id);
            if (var != null)
            {
                await _categoryRepository.Remove(var);
            }
            else
            {
                throw new Exceptions("Id invalido");
            }
            
        }

        public async Task Update( Category entity)
        {
            
                await _categoryRepository.Update(entity);
       
            
        }
      
    }
}
