using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Dtos;
using ProductService.Api.Middleware;
using ProductService.Application.Interfaces;
using ProductService.Application.Services;
using ProductService.Domain.Entities;
using System.Net;

namespace ProductService.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
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

        // GET: api/<ProductsController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAll()
        {
            var Products = await _productService.GetAllAsync() as List<Product>;
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(Products!));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetById(int id)
        {
            var Product = await _productService.GetByIdAsync(id);
            return Ok(_mapper.Map<Product, ProductDto>(Product));
        }

        // GET api/<ProductsController>/Category/5
        [HttpGet("Category/{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var Products = await _productService.GetProductsByCategoryId(categoryId) as List<Product>;
            return Ok(_mapper.Map<List<Product>, List<ProductDto>>(Products!));
        }

        // POST api/<ProductsController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Post([FromBody] ProductDto ProductDto)
        {
            var Product = await _productService.AddAsync(_mapper.Map<ProductDto, Product>(ProductDto));
            return Ok(_mapper.Map<Product, ProductDto>(Product));
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto ProductDto)
        {
            await _productService.UpdateAsync(id, _mapper.Map<ProductDto, Product>(ProductDto));
            return Ok();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.RemoveAsync(id);
            return Ok();
        }

        // DELETE api/<ProductsController>/Category/5
        [HttpDelete("Category/{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> DeleteProductsByCategoryId(int categoryId)
        {
            await _productService.RemoveProductsByCategoryId(categoryId);
            return Ok();
        }
    }
}
