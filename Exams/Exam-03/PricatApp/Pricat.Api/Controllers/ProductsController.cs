using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using System.Threading.Tasks;
using Pricat.Utilities;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService categoryService)
        {
            _productService = categoryService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        // GET api/v1/<ProductsController>/category/5
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            return Ok(await _productService.GetAllByCategoryIdAsync(id));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            // Checkeo
            if (Ean13Calculator.IsValid(product.EanCode))
            {
                await _productService.AddAsync(product);
                if (await ProductExist(product.Id))
                {
                    return Ok();
                }
                return NotFound($"La categoria con el id {product.CategoryId} no existe");
            }
            return BadRequest("El producto no tiene un código EAN valido");

        }

        // PUT api/v1/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest($"El id de {id} no coincide con la nueva entrada {product.Id}");
            }

            if (Ean13Calculator.IsValid(product.EanCode))
            {
                await _productService.UpdateAsync(id, product);
                return Ok();
            }
            return BadRequest("El producto no tiene un código EAN valido");
        }

        // DELETE api/v1/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await ProductExist(id))
            {
                await _productService.RemoveAsync(id);
                return Ok();
            }
            return NotFound($"No se encontro el producto con el id {id}");
        }

        // DELETE api/v1/<ProductsController>/category/5
        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteAllByCategoryId(int id)
        {
            await _productService.RemoveAllByCategoryIdAsync(id);
            return Ok();
        }
        private async Task<bool> ProductExist(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product != null) return true;
            return false;
        }
    }
}
