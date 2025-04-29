using AutoMapper;
using Pricat.Application.DTO;
using Pricat.Application.Services.Interfaces;
using Pricat.Application.Common.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Application.Exceptions;
using Pricat.Utilities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pricat.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ProductResultDto> CreateProduct(ProductCreateDto productCreateDto)
        {
           
            var category = await _categoryRepository.GetByIdAsync(productCreateDto.CategoryId);
            if (category is null)
                throw new NotFoundException($"Category [{productCreateDto.CategoryId}] Not Found");
            if (!Ean13Calculator.IsValid(productCreateDto.EanCode))
            {
                throw new BadRequestException($"EAN Code [{productCreateDto.EanCode}] is Not Valid");
            }

            var mappedProduct = _mapper.Map<Product>(productCreateDto);

            await _productRepository.AddAsync(mappedProduct);

            var insertedProduct = await _productRepository.GetByIdAsync(mappedProduct.Id);
            if (insertedProduct == null)
            {
                throw new InternalServerErrorException("Error al recuperar el producto recién creado.");
            }

            return _mapper.Map<ProductResultDto>(insertedProduct);
        }

        public async Task<IEnumerable<ProductResultDto>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            if (products == null || !products.Any())
            {
                throw new NotFoundException("No se encontraron productos.");
            }

            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<ProductResultDto> GetProductById(int productId)
        {
            if (productId <= 0)
            {
                throw new BadRequestException("ID de producto inválido.");
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException($"Product [{productId}] Not Found");
            }

            return _mapper.Map<ProductResultDto>(product);
        }

        public async Task<IEnumerable<ProductResultDto>> GetByCategoryId(int categoryId)
        {
            var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);
            if (products == null || !products.Any())
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }

            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<ProductResultDto> UpdateProduct(int productId, ProductCreateDto productUpdateDto)

        {
            var category = await _categoryRepository.GetByIdAsync(productUpdateDto.CategoryId);
            if (category is null)
                throw new NotFoundException($"Category [{productUpdateDto.CategoryId}] Not Found");
            if (!Ean13Calculator.IsValid(productUpdateDto.EanCode))
            {
                throw new BadRequestException($"EAN Code [{productUpdateDto.EanCode}] is Not Valid");
            }

            var existingProduct = await _productRepository.GetByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product [{productId}] Not Found");
            }

            _mapper.Map(productUpdateDto, existingProduct);
            await _productRepository.UpdateAsync(existingProduct);

            return _mapper.Map<ProductResultDto>(existingProduct);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId <= 0)
            {
                throw new BadRequestException("ID de producto inválido.");
            }

            var existingProduct = await _productRepository.GetByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product [{productId}] Not Found");
            }

            await _productRepository.RemoveAsync(existingProduct);
            return true;
        }
    }
}
