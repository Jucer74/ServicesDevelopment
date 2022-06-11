using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
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

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null) return Ok(category);
            return NotFound();
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            await _categoryService.AddAsync(category);
            return Ok(category);
        }

        // PUT api/v1/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest($"El id de la categoria {id} no coincide con la nueva entrada {category.Id}");
            }
            if (await CategoryExist(id) != false)
            {
                return NotFound($"No se encontro la categoria con el id {id}");
            }
            await _categoryService.UpdateAsync(id, category);
            return Ok(category);

        }

        // DELETE api/v1/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await CategoryExist(id)!= false)
            {
                return NotFound($"No se encontro la categoria con el id {id}");
            }
            await _categoryService.RemoveAsync(id);
            return Ok();
        }

        private async Task<bool> CategoryExist(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null) return true;
            return false;
        }

    }
}
