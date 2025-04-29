using System.Threading.Tasks;
using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.UseCases.GetProducts;
using Pricat.Application.UseCases.GetProductById;
using System.Net;
using System.Collections.Generic;

namespace Pricat.Api.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _mediator.Send(new GetProductsQuery());
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid ID");
                }

                var product = await _mediator.Send(new GetProductByIdQuery { Id = id });

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("Category/{categoryId}/Products")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                {
                    return BadRequest("Invalid Category ID");
                }

                var products = await _mediator.Send(new GetProductsByCategoryIdQuery { CategoryId = categoryId });
                return Ok(products);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validar que el ID sea 0 (para nuevo producto)
                if (product.Id != 0)
                {
                    return BadRequest("ID must be 0 for new products");
                }

                var createdProduct = await _mediator.Send(new CreateProductCommand { Product = product });
                return Ok(createdProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != product.Id)
                {
                    return BadRequest("ID in URL does not match ID in request body");
                }

                await _mediator.Send(new UpdateProductCommand { Id = id, Product = product });
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid ID");
                }

                await _mediator.Send(new DeleteProductCommand { Id = id });
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}