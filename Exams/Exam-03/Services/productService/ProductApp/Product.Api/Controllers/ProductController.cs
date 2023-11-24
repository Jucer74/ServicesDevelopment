using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Dtos;
using Product.Api.Middleware;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService ProductService, IMapper mapper)
        {
            _ProductService = ProductService;
            _mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAll()
        {
            var products = await _ProductService.GetAllAsync() as List<EProduct>;
            return Ok(_mapper.Map<List<EProduct>, List<ProductDto>>(products!));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _ProductService.GetByIdAsync(id);
            return Ok(_mapper.Map<EProduct, ProductDto>(product));
        }

        // GET api/<ProductsController>/Category/5
        [HttpGet("Category/{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _ProductService.GetProductsByCategoryId(categoryId) as List<EProduct>;
            return Ok(_mapper.Map<List<EProduct>, List<ProductDto>>(products!));
        }

        // POST api/<ProductsController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            var product = await _ProductService.AddAsync(_mapper.Map<ProductDto, EProduct>(productDto));
            return Ok(_mapper.Map<EProduct, ProductDto>(product));
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, EProduct>(productDto);
            await _ProductService.UpdateAsync(id, product);
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
            await _ProductService.RemoveAsync(id);
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
            await _ProductService.RemoveProductsByCategoryId(categoryId);
            return Ok();
        }
    }
}