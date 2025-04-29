using Microsoft.AspNetCore.Mvc;
using Pricat.Application.DTO;
using Pricat.Application.Services.Interfaces;

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

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetCategories();
            return Ok(result);
        }

       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var result = await _categoryService.CreateCategory(categoryCreateDto);

            if (result == null)
            {
                return StatusCode(500, "Unexpected error creating the category.");
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

    
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryCreateDto categoryUpdateDto)
        {
            try
            {
                var result = await _categoryService.UpdateCategory(id, categoryUpdateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

       
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _categoryService.DeleteCategory(id);

            if (!success)
                return NotFound($"Category with ID {id} not found.");

            return NoContent();
        }
    }
}
