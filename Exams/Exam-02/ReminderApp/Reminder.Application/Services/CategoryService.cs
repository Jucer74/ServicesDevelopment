using ReminderAPP.Application.Interfaces;
using ReminderAPP.Domain.Entities;
using ReminderAPP.Domain.Interface;
using ReminderAPP.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReminderAPP.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Category entity)
        {
            await _categoryRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _categoryRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var Category = await _categoryRepository.GetByIdAsync(id);

            // Validte If Exist
            return Category;
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.RemoveAsync(category);
        }

        public async Task UpdateAsync(int id, Category entity)
        {
            // Validate if Exist
            await _categoryRepository.UpdateAsync(entity);
        }
    }
}
