using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Interfaces;
using Pricat.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


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

        // GET /api/v1.0/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET /api/v1.0/Categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST /api/v1.0/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> AddCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null || string.IsNullOrWhiteSpace(categoryCreateDto.Description))
            {
                return BadRequest("Invalid data");
            }

            var createdCategory = await _categoryService.AddCategoryAsync(categoryCreateDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        // PUT /api/v1.0/Categories/{id}
        [HttpPut] // 
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Se requiere un cuerpo válido.");
            }

            var existingCategory = await _categoryService.GetCategoryByIdAsync(categoryDto.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            await _categoryService.UpdateCategoryAsync(categoryDto);
            return NoContent();
        }

        // DELETE /api/v1.0/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _categoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
