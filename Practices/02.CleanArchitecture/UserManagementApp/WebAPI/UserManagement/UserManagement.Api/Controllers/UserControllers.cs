using Microsoft.AspNetCore.Mvc;
using UserManagement.App.Interfaces;
using UserManagement.Dom.Entities;
using UserManagement.Dom.Exceptions;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Api.Controllers
{
   [Route("api/v1/[controller]")]
   [ApiController]
   public class UserController : ControllerBase
   {
      private readonly IUserServices _userServices;

      public UserController(IUserServices userServices)
      {
         _userServices = userServices;
      }

    
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         return Ok(await _userServices.GetAllAsync());
      }

      // GET api/<PeopleController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> GetById(int id)
      {
         try
         {
            return Ok(await _userServices.GetByIdAsync(id));
         }
         catch (NotFoundException ex)
         {
            return NotFound(ex.Message);
         }
      }

      // POST api/<PeopleController>
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] User user)
      {
         return Ok(await _userServices.AddAsync(user));
      }

      // PUT api/<PeopleController>/5
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody] User user)
      {
         try
         {
            return Ok(await _userServices.UpdateAsync(id, user));
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
            await _userServices.RemoveAsync(id);
            return Ok();
         }
         catch (NotFoundException ex)
         {
            return NotFound(ex.Message);
         }
      }
   }
}