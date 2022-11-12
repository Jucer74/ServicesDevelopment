using Microsoft.AspNetCore.Mvc;
using People.Application.Interfaces;
using People.Domain.Entities;
using People.Domain.Exceptions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace People.Api.Controllers
{
   [Route("api/v1/[controller]")]
   [ApiController]
   public class PeopleController : ControllerBase
   {
      private readonly IPersonService _personService;

      public PeopleController(IPersonService personService)
      {
         _personService = personService;
      }

      // GET: api/<PeopleController>
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         return Ok(await _personService.GetAllAsync());
      }

      // GET api/<PeopleController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> GetById(int id)
      {
         try
         {
            return Ok(await _personService.GetByIdAsync(id));
         }
         catch (NotFoundException ex)
         {
            return NotFound(ex.Message);
         }
      }

      // POST api/<PeopleController>
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] Person person)
      {
         return Ok(await _personService.AddAsync(person));
      }

      // PUT api/<PeopleController>/5
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody] Person person)
      {
         try
         {
            return Ok(await _personService.UpdateAsync(id, person));
         }
         catch (BadRequestException ex)
         {
            return BadRequest(ex.Message);
         }
         catch (NotFoundException ex)
         {
            return NotFound(ex.Message);
         }
      }

      // DELETE api/<PeopleController>/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         try
         {
            await _personService.RemoveAsync(id);
            return Ok();
         }
         catch (NotFoundException ex)
         {
            return NotFound(ex.Message);
         }
      }
   }
}