
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;
using Pricat.Utilities;

namespace Pricat.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            
            if (category == null)
            {
                throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
            }

            return await _productRepository.AddAsync(product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            await _productRepository.RemoveAsync(product);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return (await _productRepository.GetAllAsync()).ToList();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            return product;
        }

        public async Task<Product> UpdateProduct(int id, Product entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Product.Id [{entity.Id}]");
            }

            var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);

            if (category == null)
            {
                throw new NotFoundException($"Category [{entity.CategoryId}] Not Found");
            }

            if (!Ean13Calculator.IsValid(entity.EanCode))
            {
                throw new BadRequestException($"EAN Code [{entity.EanCode}] is Not Valid");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }
            return await _productRepository.UpdateAsync(entity);
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            
            if (category == null)
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }

            return (await _productRepository.FindAsync(p => p.CategoryId == categoryId)).ToList();
        }
    }
}
