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

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {

            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category is null)
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }

            return (await _productRepository.FindAsync(p => p.CategoryId == categoryId)).ToList();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var original = await _productRepository.GetByIdAsync(product.Id);
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);

            if (original != null)
            {
                throw new BadRequestException($"Product [{product.Id}] Not Found");
            }

            if (category is null)
            {
                throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
            }

            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            await _productRepository.AddAsync(product);

            return product;
        }

        public async Task DeleteProductr(int id)
        {
            var original = await _productRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            await _productRepository.RemoveAsync(original);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return (await _productRepository.GetAllAsync()).ToList();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            return product!;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var original = await _productRepository.GetByIdAsync(id);

            if (id != product.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Product.Id [{product.Id}]");
            }

            if (original is null)
            {
                throw new NotFoundException($"Product [{product.Id}] Not Found");
            }

            if (category is null)
            {
                throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
            }

            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            await _productRepository.UpdateAsync(product);

            return product!;
        }
    }
}
