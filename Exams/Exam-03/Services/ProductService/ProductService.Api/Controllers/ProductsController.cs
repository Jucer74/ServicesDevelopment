using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Middleware;
using ProductService.Application.dto;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Product>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ProductService.GetAllAsync());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Product))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ProductService.GetByIdAsync(id));
        }

        // GET api/<ProductsController>/Category/5
        [HttpGet("Category/{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Product>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            return Ok(await _ProductService.GetProductsByCategoryId(categoryId));
        }

        // POST api/<ProductsController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Product))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Post([FromBody] ProductDto Product)
        {
            return Ok(await _ProductService.AddAsync(Product));
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto Product)
        {
            await _ProductService.UpdateAsync(id, Product);
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
    }
}