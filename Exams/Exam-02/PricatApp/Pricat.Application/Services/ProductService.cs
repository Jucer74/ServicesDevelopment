using Application.Exceptions;
using PricatApp.Application.Interfaces;
using PricatApp.Domain.Entities;
using PricatApp.Domain.Exceptions;
using PricatApp.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PricatApp.Application.Services
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository employeeRepository)
        {
            _productRepository = employeeRepository;
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
            var product = await _productRepository.GetByIdAsync(id);

            // Validte If Exist
            return product;
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Product with Id={id} Not Found");
            }

            await _productRepository.RemoveAsync(product);
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            if (id != entity.Id)
            {

                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Product with Id={id} Not Found");
            }
            return (await _productRepository.UpdateAsync(entity));
        }
    }
}