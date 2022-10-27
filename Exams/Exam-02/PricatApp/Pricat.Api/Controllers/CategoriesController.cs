using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using PricatApp.Application.Interfaces;
using PricatApp.Domain.Entities;
using PricatApp.Domain.Exceptions;

namespace Pricat.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategorieService _categorieService;
        public CategoriesController(ICategorieService categorieService)
        {
            _categorieService = categorieService;
        }

        // GET: api/<CategorieController>
        [HttpGet]
        //[ProducesDefaultResponseType((int)HttpStatusCode.OK, Type= typeof(List<category>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categorieService.GetAllAsync());
        }

        // GET api/<CategorieController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categorieService.GetByIdAsync(id));
        }

        // POST api/<CategorieController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categorie categorie)
        {
            return Ok(await _categorieService.AddAsync(categorie));
        }

        // PUT api/<CategorieController
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Categorie categorie)
        {
            try
            {
                return Ok(await _categorieService.UpdateAsync(id, categorie));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<CategorieController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categorieService.RemoveAsync(id);
            return Ok();
        }
    }
}