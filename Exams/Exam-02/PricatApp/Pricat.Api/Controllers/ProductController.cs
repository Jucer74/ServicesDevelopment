using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.DTOs.Product;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;
using Pricat.Infrastructure.Repositories;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/Products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

    public ProductController(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;

        }

        [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _productRepository.GetAllAsync();
        var productDto = _mapper.Map<IEnumerable<ProductDTO>>(categories);
        return Ok(productDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException($"Product {id} Not Found");
        }

        var productDto = _mapper.Map<ProductDTO>(product);
        return Ok(productDto);
    }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDTO createProductDto)
        {
            // Validación del CategoryId
            var category = await _categoryRepository.GetByIdAsync(createProductDto.CategoryId);
            if (category is null)
            {
                throw new NotFoundException($"Category {createProductDto.CategoryId} Not Found");
            }

            var product = _mapper.Map<Product>(createProductDto);
            var createdProduct = await _productRepository.AddAsync(product);
            var productDto = _mapper.Map<ProductDTO>(createdProduct);

            return Ok(productDto);
        }


        [HttpGet("Category/{categoryId}")]

        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);

            if (products == null || !products.Any())
                return NotFound($"Category [{categoryId}] Not Found");

            var productDtos = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDtos);
        }

        [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProductDTO updateProductDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException($"Product {id} Not Found");
        }

        _mapper.Map(updateProductDto, product);
        var updatedProduct = await _productRepository.UpdateAsync(product);
        var productDto = _mapper.Map<ProductDTO>(updatedProduct);

        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException($"Product {id} Not Found");
        }

        await _productRepository.RemoveAsync(product);
        return Ok();
    }
}
}
