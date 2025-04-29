using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Pricat.Application.Services;
using Pricat.Domain.Entities;

namespace Pricat.Api.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {
                    ErrorType = "Bad Request",
                    Errors = ModelState.Values
                                      .SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList()
                });
            }

            var created = await _service.CreateAsync(product);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {
                    ErrorType = "Bad Request",
                    Errors = ModelState.Values
                                      .SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList()
                });
            }

            product.Id = id;
            await _service.UpdateAsync(product);
            return Ok();
        }

        // ... (otros endpoints GET/DELETE sin cambios de ModelState)
    }
}
