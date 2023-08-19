using Microsoft.AspNetCore.Mvc;
using Student.Application.Interfaces;
using Student.Domain.Entities;
using Student.Domain.Exceptions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Student.Api.Controllers
{
   [Route("api/v1/[controller]")]
   [ApiController]
   public class StudentController : ControllerBase
   {
      private readonly IStudentService _StudentService;

      public StudentController(IStudentService StudentService)
      {
         _StudentService = StudentService;
      }

      // GET: api/<StudentController>
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         return Ok(await _StudentService.GetAllAsync());
      }

      // GET api/<StudentController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> GetById(int id)
      {
         try
         {
            return Ok(await _StudentService.GetByIdAsync(id));
         }
         catch (NotFoundException ex)
         {
            return NotFound(ex.Message);
         }
      }

      // POST api/<StudentController>
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] Student Student)
      {
         return Ok(await _StudentService.AddAsync(Student));
      }

      // PUT api/<StudentController>/5
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody] Student Student)
      {
         try
         {
            return Ok(await _StudentService.UpdateAsync(id, Student));
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

      // DELETE api/<StudentController>/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         try
         {
            await _StudentService.RemoveAsync(id);
            return Ok();
         }
         catch (NotFoundException ex)
         {
            return NotFound(ex.Message);
         }
      }
   }
}