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

        // GET: api/v1.0/Categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetCategories();
            return Ok(result);
        }

        // GET: api/v1.0/Categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryService.GetCategoryById(id);
            return Ok(result);
        }

        // POST: api/v1.0/Categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var result = await _categoryService.CreateCategory(categoryCreateDto);
            return Ok(result);
        }

        // PUT: api/v1.0/Categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryCreateDto categoryUpdateDto)
        {
            var result = await _categoryService.UpdateCategory(id, categoryUpdateDto);
            return Ok(result);
        }

        // DELETE: api/v1.0/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
