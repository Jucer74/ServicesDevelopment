using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(_mapper.Map<List<Category>, List<CategoryDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var team = await _categoryService.GetCategoryById(id);
            return Ok(_mapper.Map<Category, CategoryDto>(team));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            var category = await _categoryService.CreateCategory(_mapper.Map<CategoryDto, Category>(categoryDto));
            return Ok(_mapper.Map<Category, CategoryDto>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = await _categoryService.UpdateCategory(id, _mapper.Map<CategoryDto, Category>(categoryDto));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
