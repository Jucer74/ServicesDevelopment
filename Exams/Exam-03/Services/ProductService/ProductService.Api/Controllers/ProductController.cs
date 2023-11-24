using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Dtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using ProductService.Domain.Exceptions;

namespace ProductService.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService categoryProductService, IMapper mapper)
        {
            _productService = categoryProductService;
            _mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var categoryProducts = await _productService.GetAllProducts() as List<Product>;
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(categoryProducts!));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var categoryProduct = await _productService.GetProductById(id);
            return Ok(_mapper.Map<Product, ProductDto>(categoryProduct));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto categoryProductDto)
        {
            var categoryProduct = await _productService.AddProduct(_mapper.Map<ProductDto, Product>(categoryProductDto));
            return Ok(_mapper.Map<Product, ProductDto>(categoryProduct));
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto categoryProductDto)
        {
            var categoryProduct = await _productService.UpdateProduct(id, _mapper.Map<ProductDto, Product>(categoryProductDto));
            return Ok(_mapper.Map<Product, ProductDto>(categoryProduct));
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.RemoveProduct(id);
            return Ok();
        }

        // GET api/<ProductsController>/Category/5
        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var categoryProducts = await _productService.GetProductsByCategoryId(categoryId) as List<Product>;
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(categoryProducts!));
        }

        // DELETE /<ProductsController>/category/5
        [HttpDelete("category/{categoryId}")]
        public async Task<IActionResult> DeleteByCategory(int categoryId)
        {
            try
            {
                await _productService.RemoveProductsByCategoryId(categoryId);
                return Ok($"Los productos asociados a la categoría con ID {categoryId} han sido eliminados correctamente.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar los productos de la categoría: {ex.Message}");
            }
        }

    }
}