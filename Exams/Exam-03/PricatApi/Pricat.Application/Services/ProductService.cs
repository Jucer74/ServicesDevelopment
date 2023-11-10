using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.Exceptions;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricat.Application.Services
{
   public class ProductService : IProductService
   {
      private readonly IProductRepository _productRepository;
      private readonly ICategoryRepository _categoryRepository;

      public ProductService(IProductRepository ProductRepository, ICategoryRepository categoryRepository)
      {
         _productRepository = ProductRepository;
         _categoryRepository = categoryRepository;
      }

      public async Task<Product> AddAsync(Product product)
      {
         if(!Ean13Calculator.IsValid(product.EanCode))
         {
            throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
         }

         if (!await CategoryExists(product.CategoryId))
         {
            throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
         }

         return await _productRepository.AddAsync(product);
      }

      public async Task<IEnumerable<Product>> GetAllAsync()
      {
         return await _productRepository.GetAllAsync();
      }

      public async Task<Product> GetByIdAsync(int id)
      {
         var product = await _productRepository.GetByIdAsync(id);

         if (product is null)
         {
            throw new NotFoundException($"Product [{id}] Not Found");
         }

         return product;
      }

      public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
      {
         if (!await CategoryExists(categoryId))
         {
            throw new NotFoundException($"Category [{categoryId}] Not Found");
         }

         return await _productRepository.FindAsync(p => p.CategoryId == categoryId);
      }

      public async Task RemoveAsync(int id)
      {
         var product = await _productRepository.GetByIdAsync(id);
         if (product is null)
         {
            throw new NotFoundException($"Product [{id}] Not Found");
         }

         await _productRepository.RemoveAsync(product);
      }

      public async Task UpdateAsync(int id, Product product)
      {
         if (id != product.Id)
         {
            throw new BadRequestException($"Id [{id}] is different to Product.Id [{product.Id}]");
         }

         if (!Ean13Calculator.IsValid(product.EanCode))
         {
            throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
         }

         if (!await CategoryExists(product.CategoryId))
         {
            throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
         }

         await _productRepository.UpdateAsync(product);
      }

      private async Task<bool> CategoryExists(int categoryId)
      {
         var category = await _categoryRepository.GetByIdAsync(categoryId);

         return (category is not null);
      }
   }
}