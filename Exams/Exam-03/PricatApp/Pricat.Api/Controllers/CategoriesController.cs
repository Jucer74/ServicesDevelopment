using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pricat.Application.interfaces;
using Pricat.Domain.Entities;
using System.Threading.Tasks;


namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoryService;


        public CategoriesController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            if (_categoryService.GetAll() != null)
            {
                return Ok(await _categoryService.GetAll());
                
            }
            else
            {
                throw new InternalServerErrorException("error");
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);

            try
            {
                if (category != null)
                {
                    return Ok(category);
                    
                }
                else
                {
                    throw new BusinessException("error");
                }
            }
            catch
            {
                throw new InternalServerErrorException("error");
            }
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> add(Categories _category)
        {
            await _categoryService.Add(_category);
            return Ok();
        }

        // PUT api/v1/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Categories category)
        {
              
            if (category != null)
            {
                await _categoryService.Update(category);
                return Ok(category);
            }
            else
            {
                throw new BusinessException("");
            }

        }

        // DELETE api/v1/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await CategoryExist(id) != false)
            {
                await _categoryService.Remove(id);
                return Ok();
            }
            else
            {
                throw new NotFoundException("error");
            }
           
        }

        private async Task<bool> CategoryExist(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category != null) {
                return true;
               }
            else {
                return false;
            }
        }

    }
}
