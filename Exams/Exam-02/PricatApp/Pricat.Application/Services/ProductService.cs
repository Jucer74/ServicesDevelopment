using System.Threading.Tasks;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using Pricat.Application.Exceptions;
using Pricat.Utilities; // para Ean13Calculator

namespace Pricat.Application.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            // 1. Validar EAN
            if (!Ean13Calculator.IsValid(product.EanCode))
                throw new BadRequestException("EAN Code is Not Valid");

            // 2. Validar existencia de categoría
            if (!await _categoryRepository.ExistsAsync(c => c.Id == product.CategoryId))
                throw new NotFoundException($"Category {product.CategoryId} Not Found");

            // 3. Agregar y devolver
            return await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            // Verificar si el producto existe
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"El producto con ID {product.Id} no existe.");
            }

            // Actualizar las propiedades del producto existente
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.EanCode = product.EanCode;
            existingProduct.Description = product.Description;
            existingProduct.Unit = product.Unit;
            existingProduct.Price = product.Price;

            // Actualizar en el repositorio
            await _productRepository.UpdateAsync(existingProduct);
        }
        // Implementa GetAll, GetById, Update y Delete de forma análoga,
        // usando ExistsAsync y NotFoundException o BadRequestException
    }
}
