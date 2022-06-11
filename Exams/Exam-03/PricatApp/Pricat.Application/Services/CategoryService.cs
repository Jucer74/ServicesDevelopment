using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pricat.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
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
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;
            return category;
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
                await _productRepository.RemoveAllByCategoryIdAsync(id);
                await _categoryRepository.RemoveAsync(category);
            }

        public async Task UpdateAsync(int id, Category entity)
        {
            await _categoryRepository.UpdateAsync(entity);
        }

    }

}