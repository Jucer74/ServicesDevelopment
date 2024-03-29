﻿using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.Exceptions;
using Pricat.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pricat.Application.Services
{
   public class CategoryService : ICategoryService
   {
      private readonly ICategoryRepository _categoryRepository;
      private readonly IProductRepository _productRepository;

      public CategoryService(ICategoryRepository CategoryRepository, IProductRepository productRepository)
      {
         _categoryRepository = CategoryRepository;
         _productRepository = productRepository;
      }

      public async Task<Category> AddAsync(Category category)
      {
         return await _categoryRepository.AddAsync(category);
      }

      public async Task<IEnumerable<Category>> GetAllAsync()
      {
         return await _categoryRepository.GetAllAsync();
      }

      public async Task<Category> GetByIdAsync(int id)
      {
         var category = await _categoryRepository.GetByIdAsync(id);

         if (category is null)
         {
            throw new NotFoundException($"Category [{id}] Not Found");
         }

         return category;
      }

      public async Task RemoveAsync(int id)
      {
         var products = await GetProductsByCategoryIdAsync(id);
         if (products.Any())
         {
            await _productRepository.RemoveRangeAsync(products);
         }

         var category = await _categoryRepository.GetByIdAsync(id);
         if (category is null)
         {
            throw new NotFoundException($"Category [{id}] Not Found");
         }

         await _categoryRepository.RemoveAsync(category);
      }

      public async Task UpdateAsync(int id, Category category)
      {
         if (id != category.Id)
         {
            throw new BadRequestException($"Id [{id}] is different to Category.Id [{category.Id}]");
         }

         await _categoryRepository.UpdateAsync(category);
      }

      private async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
      {
         return await _productRepository.FindAsync(p => p.CategoryId == categoryId);
      }
   }
}