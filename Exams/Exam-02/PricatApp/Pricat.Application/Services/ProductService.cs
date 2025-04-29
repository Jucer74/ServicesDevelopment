using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.DTOs;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using Pricat.Application.Services;
using Pricat.Domain.Exceptions;
using AutoMapper;

namespace Pricat.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> AddAsync(ProductDto productDto)
        {
            if (!await _categoryRepository.ExistsAsync(productDto.CategoryId))
                throw new NotFoundException("Category not found");

            if (!await _productRepository.IsEanCodeUniqueAsync(productDto.EanCode))
                throw new DomainException("DUPLICATE_EAN", "EAN Code must be unique");

            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<ProductDto?> UpdateAsync(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
                throw new BadRequestException("ID mismatch");

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null) return null;

            if (existingProduct.CategoryId != productDto.CategoryId &&
                !await _categoryRepository.ExistsAsync(productDto.CategoryId))
                throw new NotFoundException("New category not found");

            if (existingProduct.EanCode != productDto.EanCode &&
                !await _productRepository.IsEanCodeUniqueAsync(productDto.EanCode, id))
                throw new DomainException("DUPLICATE_EAN", "EAN Code must be unique");

            _mapper.Map(productDto, existingProduct);
            await _productRepository.UpdateAsync(existingProduct);

            return _mapper.Map<ProductDto>(existingProduct);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new NotFoundException("Product not found");

            await _productRepository.DeleteAsync(product);
        }

        Task<IEnumerable<Product>> IProductService.GetByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEanCodeUniqueAsync(string eanCode, int? excludedId = null)
        {
            throw new NotImplementedException();
        }
    }
}