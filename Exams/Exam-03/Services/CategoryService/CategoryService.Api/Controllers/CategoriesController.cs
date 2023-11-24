using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CategoryService.Application.Interfaces;
using CategoryService.Domain.Entities;
using CategoryService.Api.Dtos;

namespace CategoryService.Api.Controllers
{
    [Route("api/v1.0/[controller]")]

    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryServic;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryServic, IMapper mapper)
        {
            _categoryServic = categoryServic;
            _mapper = mapper;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categorys = await _categoryServic.GetAllCategories() as List<Category>;
            return Ok(_mapper.Map<List<Category>, List<CategoryDto>>(categorys!));
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var Category = await _categoryServic.GetCategoryById(id);
            return Ok(_mapper.Map<Category, CategoryDto>(Category));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            var category = await _categoryServic.AddCategory(_mapper.Map<CategoryDto, Category>(categoryDto));
            return Ok(_mapper.Map<Category, CategoryDto>(category));
        }


        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = await _categoryServic.UpdateCategory(id, _mapper.Map<CategoryDto, Category>(categoryDto));
            return Ok(_mapper.Map<Category, CategoryDto>(category));
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryServic.RemoveCategory(id);
            return Ok();
        }

        // GET api/<CategoriesController>/5/Members
        [HttpGet("{categoryId}/Products")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _categoryServic.GetProductByCategoryId(categoryId);
            return Ok(products);
        }
    }
}