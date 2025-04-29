using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(products));
        }

        [HttpGet("Products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        [HttpPost("Products")]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            var product = await _productService.CreateProduct(_mapper.Map<ProductDto, Product>(productDto));
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        [HttpPut("Products/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            var product = await _productService.UpdateProduct(id, _mapper.Map<ProductDto, Product>(productDto));
            return Ok();
        }

        [HttpDelete("Products/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductr(id);
            return Ok();
        }

        [HttpGet("Products/Category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategory(categoryId);
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(products));
        }
    }
}
