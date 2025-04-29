using System.Threading.Tasks;
using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.UseCases.GetCategories;
using Pricat.Application.UseCases.CreateCategory;
using Pricat.Application.UseCases.UpdateCategory;
using Pricat.Domain.Entities;
using System.Net;
using System.Collections.Generic;

namespace Pricat.Api.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _mediator.Send(new GetCategoriesQuery());
                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdCategory = await _mediator.Send(new CreateCategoryCommand { Category = category });
                return Ok(createdCategory);
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
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != category.Id)
                {
                    return BadRequest("ID in URL does not match ID in request body");
                }

                await _mediator.Send(new UpdateCategoryCommand { Id = id, Category = category });
                return Ok();
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

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteCategoryCommand { Id = id });
                return Ok();
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


    }
}


