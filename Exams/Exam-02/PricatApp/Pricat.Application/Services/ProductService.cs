using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProduct(Product product)
        {

            await _productRepository.AddAsync(product);

            return product;
        }

        public async Task DeleteProductr(int id)
        {
            var original = await _productRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Product with Id={id} Not Found");
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
                throw new NotFoundException($"Product with Id={id} Not Found");
            }

            return product!;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Product.Id [{product.Id}]");
            }

            var original = await _productRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Product with Id={id} Not Found");
            }

            await _productRepository.UpdateAsync(product);

            return product!;
        }
    }
}
