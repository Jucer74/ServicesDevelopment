using ProductService.Application.dto;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Exceptions;
using ProductService.Domain.Interfaces.Proxies;
using ProductService.Domain.Interfaces.Repositories;
using ProductService.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryProxy _categoryProxy;

        public ProductService(IProductRepository ProductRepository, ICategoryProxy categoryProxy)
        {
            _productRepository = ProductRepository;
            _categoryProxy = categoryProxy;
        }

        public async Task<Product> AddAsync(ProductDto product)
        {
            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            if (!await CategoryExists(product.CategoryId))
            {
                throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
            }
            Category gettedCategory = await _categoryProxy.GetByIdAsync(product.CategoryId);
            Product productToCreate = new()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                CategoryName = gettedCategory.Description,
                EanCode = product.EanCode,
                Description = product.Description,
                Unit = product.Unit,
                Price = product.Price
            };

            return await _productRepository.AddAsync(productToCreate);
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

        public async Task UpdateAsync(int id, ProductDto product)
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

            Product productToUpdate = await GetByIdAsync(product.Id);
            productToUpdate.Id = product.Id;
            productToUpdate.EanCode = product.EanCode;
            productToUpdate.Description = product.Description;
            productToUpdate.Unit = product.Unit;
            productToUpdate.Price = product.Price;
            if (product.CategoryId != productToUpdate.CategoryId)
            {
                Category gettedCategory = await _categoryProxy.GetByIdAsync(product.CategoryId);
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.CategoryName = gettedCategory.Description;
            }
            await _productRepository.UpdateAsync(productToUpdate);
        }

        private async Task<bool> CategoryExists(int categoryId)
        {
            var category = await _categoryProxy.GetByIdAsync(categoryId);

            return (category is not null);
        }
    }
}