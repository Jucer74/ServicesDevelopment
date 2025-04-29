using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.DTOs;
using Pricat.Application.Execptions;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoriService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoriService, IMapper mapper)
        {
            _categoriService = categoriService;
            _mapper = mapper;
        }

        // GET: api/<CategoriController>
        [HttpGet]
        public async Task<IActionResult> GetAllCategori()
        {
            var categori = await _categoriService.GetAllAsync();
            return Ok(_mapper.Map<List<CategoryDto>>(categori));
        }

        // GET api/<CategoriController>/5
        [HttpGet("{id}")]
        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoriService.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException($"Category [{id}] Not Found");

            return _mapper.Map<CategoryDto>(category);
        }


        // POST api/<CategoriController>
        [HttpPost]
        public async Task<IActionResult> AddCategori([FromBody] Categories categori)
        {
            if (categori == null)
            {
                throw new BadRequestException("User is null");
            }
            var createdCategori = await _categoriService.AddAsync(categori);
            return Ok(_mapper.Map<CrearCategoryDto>(createdCategori));
        }

        // PUT api/<CategoriController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategori(int id, [FromBody] Categories categori)
        {
            if (id != categori.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{categori.Id}]");
            }

            categori.Id = id;

            var updatedCategori = await _categoriService.UpdateAsync(id, categori);
            return Ok(_mapper.Map<CrearCategoryDto>(updatedCategori));
        }


        // DELETE api/<CategoriController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategori(int id)
        {
            var categori = await _categoriService.GetByIdAsync(id);
            if (categori == null)
            {
                throw new NotFoundException($"Entity with Id={id} not found");
            }
            await _categoriService.DeleteAsync(id);
            return Ok();
        }
    }
}
