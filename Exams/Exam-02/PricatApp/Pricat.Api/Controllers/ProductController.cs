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

        // GET: api/v1.0/Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetProducts();
            return Ok(result);
        }

        // GET: api/v1.0/Products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetProductById(id);
            return Ok(result);
        }

        // GET: api/v1.0/Products/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _productService.GetByCategoryId(categoryId);
            return Ok(result);
        }

        // POST: api/v1.0/Products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            var result = await _productService.CreateProduct(productCreateDto);
            return Ok(result);
        }

        // PUT: api/v1.0/Products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDto productUpdateDto)
        {
            var result = await _productService.UpdateProduct(id, productUpdateDto);
            return Ok(result);
        }

        // DELETE: api/v1.0/Products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
