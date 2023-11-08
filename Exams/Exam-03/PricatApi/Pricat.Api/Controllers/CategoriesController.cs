using Microsoft.AspNetCore.Mvc;
using Pricat.Api.Middleware;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pricat.Api.Controllers
{
   [Route("api/v1.0/[controller]")]
   [ApiController]
   public class CategoriesController : ControllerBase
   {
      private readonly ICategoryService _categoryService;

      public CategoriesController(ICategoryService categoryService)
      {
         _categoryService = categoryService;
      }

      // GET: api/<CategoriesController>
      [HttpGet]
      [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Category>))]
      [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
      public async Task<IActionResult> GetAll()
      {
         return Ok(await _categoryService.GetAllAsync());
      }

      // GET api/<CategoriesController>/5
      [HttpGet("{id}")]
      [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Category))]
      [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
      [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
      [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
      public async Task<IActionResult> GetById(int id)
      {
         return Ok(await _categoryService.GetByIdAsync(id));
      }

      // POST api/<CategoriesController>
      [HttpPost]
      [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Category))]
      [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
      [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
      public async Task<IActionResult> Post([FromBody] Category category)
      {
         return Ok(await _categoryService.AddAsync(category));
      }

      // PUT api/<CategoriesController>/5
      [HttpPut("{id}")]
      [ProducesResponseType((int)HttpStatusCode.OK)]
      [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
      [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
      [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
      public async Task<IActionResult> Put(int id, [FromBody] Category category)
      {
         await _categoryService.UpdateAsync(id, category);
         return Ok();
      }

      // DELETE api/<CategoriesController>/5
      [HttpDelete("{id}")]
      [ProducesResponseType((int)HttpStatusCode.OK)]
      [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
      [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
      [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
      public async Task<IActionResult> Delete(int id)
      {
         await _categoryService.RemoveAsync(id);
         return Ok();
      }
   }
}