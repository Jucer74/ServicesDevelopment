using System.Globalization;
using System.Linq.Expressions;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;

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

        public async Task<Product> AddAsync(Product entity)
        {
            return await _productRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }


        public async Task RemoveAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id); 
            await _productRepository.RemoveAsync(product);
        }


        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"Id {id} is different to Category.Id  {entity.Id}");
            }

            await _productRepository.GetByIdAsync(id);

            // Verifica que la categoría exista
            var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
            if (category is null)
            {
                throw new NotFoundException($"Category {entity.CategoryId} Not Found");
            }

            return await _productRepository.UpdateAsync(entity);
        }
    }
}