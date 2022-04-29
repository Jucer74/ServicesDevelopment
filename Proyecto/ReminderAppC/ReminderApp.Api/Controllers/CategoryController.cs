using Microsoft.AspNetCore.Mvc;
using ReminderApp.Application.Interfaces;
using ReminderApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReminderApp.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CategoryController : ControllerBase
   {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<PeopleController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
        }

        // POST api/<PeopleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category person)
        {
            await _categoryService.AddAsync(person);
            return Ok();
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category person)
        {
            await _categoryService.UpdateAsync(id, person);
            return Ok();
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(id);
            return Ok();
        }
    }
}
