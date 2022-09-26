using EmployeeApp.Application.Interfaces;
using EmployeeApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Api.Controllers
{
   [Route("api/v1/[controller]")]
   [ApiController]
   public class EmployeesController : ControllerBase
   {
      private readonly IEmployeeService _EmployeeService;

      public EmployeesController(IEmployeeService EmployeeService)
      {
         _EmployeeService = EmployeeService;
      }

      // GET: api/<PeopleController>
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         return Ok(await _EmployeeService.GetAllAsync());
      }

      // GET api/<PeopleController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> GetById(int id)
      {
         return Ok(await _EmployeeService.GetByIdAsync(id));
      }

      // POST api/<PeopleController>
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] Employee Employee)
      {
         return Ok(await _EmployeeService.AddAsync(Employee));
      }

      // PUT api/<PeopleController>/5
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody] Employee Employee)
      {
         await _EmployeeService.UpdateAsync(id, Employee);
         return Ok();
      }

      // DELETE api/<PeopleController>/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         await _EmployeeService.RemoveAsync(id);
         return Ok();
      }
   }
}