using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.DTOs.Category;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoryDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

    

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDTO createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);

            var createdCategory = await _categoryRepository.AddAsync(category);

            if (createdCategory == null)
            {
                throw new NotFoundException($"category not found.");
            }

            var categoryDto = _mapper.Map<CategoryDTO>(createdCategory);

            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryDTO updateCategoryDto)
        {
            // Validar que el id del body coincida con el id de la ruta
            if (id != updateCategoryDto.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{updateCategoryDto.Id}]");
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            _mapper.Map(updateCategoryDto, category);
            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            var categoryDto = _mapper.Map<CategoryDTO>(updatedCategory);

            return Ok(categoryDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            await _categoryRepository.RemoveAsync(category);
            return Ok();
        }
    }
}
