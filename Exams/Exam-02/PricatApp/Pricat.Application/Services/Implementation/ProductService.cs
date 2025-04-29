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
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResultDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            if (!Ean13Calculator.IsValid(productCreateDto.EanCode))
            {
                throw new BadRequestException("El código EAN proporcionado no es válido.");
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
                throw new NotFoundException($"No se encontró el producto con ID {productId}.");
            }

            return _mapper.Map<ProductResultDto>(product);
        }

        public async Task<IEnumerable<ProductResultDto>> GetByCategoryId(int categoryId)
        {
            var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);
            if (products == null || !products.Any())
            {
                throw new NotFoundException($"No se encontraron productos para la categoría con ID {categoryId}.");
            }

            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<ProductResultDto> UpdateProduct(int productId, ProductCreateDto productUpdateDto)
        {
            if (!Ean13Calculator.IsValid(productUpdateDto.EanCode))
            {
                throw new BadRequestException("El código EAN proporcionado no es válido.");
            }

            var existingProduct = await _productRepository.GetByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new NotFoundException($"No se encontró el producto con ID {productId}.");
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
                throw new NotFoundException($"El producto con ID {productId} no fue encontrado para eliminar.");
            }

            await _productRepository.RemoveAsync(existingProduct);
            return true;
        }
    }
}
