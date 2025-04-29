using System.Linq.Expressions;
using Pricat.Application.Interfaces;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;

namespace Pricat.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Products> AddAsync(Products entity)
        {
            return await _productRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Products>> FindAsync(Expression<Func<Products, bool>> predicate)
        {
            return await _productRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception($"Product with Id={id} Not Found");
            }
            return product;
        }

        public async Task<Products> UpdateAsync(int id, Products entity)
        {
            if (id != entity.Id)
            {
                throw new Exception($"Product with Id={id} mismatch.");
            }

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception($"Product with Id={id} Not Found");
            }

            return await _productRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception($"Product with Id={id} Not Found");
            }

            await _productRepository.DeleteAsync(product);
        }
    }
}
