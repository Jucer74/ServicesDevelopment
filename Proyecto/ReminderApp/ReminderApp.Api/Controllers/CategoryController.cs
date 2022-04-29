using Microsoft.AspNetCore.Mvc;
using ReminderApp.Application.Interfaces;
using ReminderApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }

  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categoryService.GetById(id));
        }

     
        [HttpPost]
        public async Task<IActionResult> add(Category _category)
        {
            await _categoryService.Add(_category);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put( Category _category)
        {
            await _categoryService.Update(_category);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Remove(id);
            return Ok();
        }
    }
}
