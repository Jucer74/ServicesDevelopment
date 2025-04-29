using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using AutoMapper;
using Pricat.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<TeamsController>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(_mapper.Map<List<Category>, List<CategoryDto>>(categories));
        }

        // GET api/<TeamsController>/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categories = await _categoryService.GetCategoryById(id);
            return Ok(_mapper.Map<Category, CategoryDto>(categories));
        }

        // POST api/<TeamsController>/:id
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            var category = await _categoryService.CreateCategory(_mapper.Map<CategoryDto, Category>(categoryDto));
            return Ok(_mapper.Map<Category, CategoryDto>(category));
        }

        // PUT api/<TeamsController>/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
        {
            var team = await _categoryService.CategoryUpdate(id, _mapper.Map<CategoryDto, Category>(categoryDto));
            return Ok(_mapper.Map<Category, CategoryDto>(team));
        }

        // DELETE api/<TeamsController>/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }
        
    }
}