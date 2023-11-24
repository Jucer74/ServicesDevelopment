using AutoMapper;
using CategoryService.Api.Dtos;
using CategoryService.Api.Middleware;
using CategoryService.Application.Interfaces;
using CategoryService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CategoryService.Api.Controllers
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

        // GET: api/<CategoriesController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<CategoryDto>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetAll()
        {
            var Categorys = await _categoryService.GetAllAsync() as List<Category>;
            return Ok(_mapper.Map<List<Category>, List<CategoryDto>>(Categorys!));
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CategoryDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetById(int id)
        {
            var Category = await _categoryService.GetByIdAsync(id);
            return Ok(_mapper.Map<Category, CategoryDto>(Category));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CategoryDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Post([FromBody] CategoryDto CategoryDto)
        {
            var Category = await _categoryService.AddAsync(_mapper.Map<CategoryDto, Category>(CategoryDto));
            return Ok(_mapper.Map<Category, CategoryDto>(Category));
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto CategoryDto)
        {
            await _categoryService.UpdateAsync(id, _mapper.Map<CategoryDto, Category>(CategoryDto));
            return Ok();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(id);
            return Ok();
        }
    }
}
