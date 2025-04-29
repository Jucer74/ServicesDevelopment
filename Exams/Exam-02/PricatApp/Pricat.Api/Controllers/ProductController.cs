using Microsoft.AspNetCore.Mvc;
using Pricat.Application.DTO;
using Pricat.Application.Services.Interfaces;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetProducts();
            return Ok(result);
        }

        // GET: api/Products/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _productService.GetProductById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Products/by-category/{categoryId}
        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _productService.GetByCategoryId(categoryId);

            if (result == null || !result.Any())
                return NotFound($"No products found for category with ID {categoryId}.");

            return Ok(result);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            var result = await _productService.CreateProduct(productCreateDto);

            if (result == null)
            {
                return StatusCode(500, "Unexpected error creating the product.");
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDto productUpdateDto)
        {
            try
            {
                var result = await _productService.UpdateProduct(id, productUpdateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteProduct(id);

            if (!success)
                return NotFound($"Product with ID {id} not found.");

            return NoContent();
        }
    }
}
