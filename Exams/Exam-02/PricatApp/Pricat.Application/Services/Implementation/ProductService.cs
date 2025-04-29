using AutoMapper;
using Pricat.Application.DTO;
using Pricat.Application.Services.Interfaces;
using Pricat.Application.Common.Interfaces;
using Pricat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pricat.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResultDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            var mappedProduct = _mapper.Map<Product>(productCreateDto);

            await _productRepository.AddAsync(mappedProduct);

            
            var insertedProduct = await _productRepository.GetByIdAsync(mappedProduct.Id);

            if (insertedProduct == null)
                throw new Exception("Error fetching the created product.");

            return _mapper.Map<ProductResultDto>(insertedProduct);
        }

        public async Task<IEnumerable<ProductResultDto>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<ProductResultDto> GetProductById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new Exception($"Category with ID {productId} not found.");
            }

            return _mapper.Map<ProductResultDto>(product);
        }

        public async Task<IEnumerable<ProductResultDto>> GetByCategoryId(int categoryId)
        {
            var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<ProductResultDto> UpdateProduct(int productId, ProductCreateDto productUpdateDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productId);

            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }

            
            _mapper.Map(productUpdateDto, existingProduct);

            await _productRepository.UpdateAsync(existingProduct);

            return _mapper.Map<ProductResultDto>(existingProduct);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productId);

            if (existingProduct == null)
            {
                return false;
            }

            await _productRepository.RemoveAsync(existingProduct);

            return true;
        }
    }
}
