using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<MembersController>
        [HttpGet("Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(products));
        }

        // GET api/<MembersController>/5
        [HttpGet("Products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        // POST api/<MembersController>
        [HttpPost("Products")]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            var product = await _productService.CreateProduct(_mapper.Map<ProductDto, Product>(productDto));
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        // PUT api/<MembersController>/5
        [HttpPut("Products/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            var product = await _productService.UpdateProduct(id, _mapper.Map<ProductDto, Product>(productDto));
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("Products/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }

        // GET api/v1.0/Category/{categoryId}/Products
        [HttpGet("Products/Category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryId(categoryId);
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(products));
        }

    }
}