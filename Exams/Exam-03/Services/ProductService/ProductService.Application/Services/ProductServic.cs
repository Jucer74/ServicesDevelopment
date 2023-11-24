using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Exceptions;
using ProductService.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Application.Services
{
    public class ProductServic : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductServic(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProduct(Product category)
        {
            return await _productRepository.AddAsync(category);
        }

        public async Task RemoveProduct(int id)
        {
            var original = await _productRepository.GetByIdAsync(id);

            if (original is not null)
            {
                await _productRepository.RemoveAsync(original);
                return;
            }

            throw new NotFoundException($"Category Product with Id={id} Not Found");
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is not null)
            {
                return product;
            }

            throw new NotFoundException($"Category Product with Id={id} Not Found");
        }

        public async Task<Product> UpdateProduct(int id, Product category)
        {
            if (id != category.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to TeamProduct.Id [{category.Id}]");
            }

            var original = await _productRepository.GetByIdAsync(id);

            if (original is not null)
            {
                return await _productRepository.UpdateAsync(category);
            }

            throw new NotFoundException($"Category Product with Id={id} Not Found");
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productRepository.FindAsync(m => m.CategoryId == categoryId);
            return products;
        }

        public async Task RemoveProductsByCategoryId(int categoryId)
        {
            var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    await _productRepository.RemoveAsync(product);
                }
                return;
            }

            throw new NotFoundException($"No se encontraron productos asociados a la categoría con ID {categoryId}");
        }




    }
}