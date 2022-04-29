using ReminderApp.Application.Interfaces;
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
            var person = await _categoryRepository.GetByIdAsync(id);

            // Validte If Exist
            return person;
        }

        public async Task RemoveAsync(int id)
        {
            var person = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.RemoveAsync(person);
        }

        public async Task UpdateAsync(int id, Category entity)
        {
            // Validate if Exist
            await _categoryRepository.UpdateAsync(entity);
        }
    }
}
