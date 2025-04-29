using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Utilities;

namespace Pricat.Application.Services;


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
            var categoryExists = await _categoryRepository.GetByIdAsync(product.CategoryId);
    
            if (categoryExists == null)
            {
                throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
            }
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);
            if (existingProduct != null)
            {
                throw new BadRequestException($"Product with Id [{product.Id}] already exists.");
            }

            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
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
        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            var categoryExists = await _categoryRepository.GetByIdAsync(categoryId);
    
            if (categoryExists == null)
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }
            var products = await _productRepository.GetAllAsync(); 
            var categoryProducts = products.Where(p => p.CategoryId == categoryId).ToList();

            if (categoryProducts == null)
            {
                throw new NotFoundException($"No products found for Category [{categoryId}]");
            }
            

            return categoryProducts;
        }



        public async Task<Product> UpdateProduct(int id, Product entity)
        
        {
            if (!Ean13Calculator.IsValid(entity.EanCode))
            {
                throw new BadRequestException($"EAN Code [{entity.EanCode}] is Not Valid");
            }
            if (id != entity.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Product.Id [{entity.Id}]");
            }
            var categoryExists = await _categoryRepository.GetByIdAsync(entity.CategoryId);
    
            if (categoryExists == null)
            {
                throw new NotFoundException($"Category [{entity.CategoryId}] Not Found");
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }
            

            return await _productRepository.UpdateAsync(entity);
        }
}
