using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;

namespace Pricat.Api.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _categoryService.GetAllAsync());
    }
    
    // GET: api/v1.0/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _categoryService.GetByIdAsync(id));
    }
    
    // POST: api/v1.0/Categories
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
        return Ok(await _categoryService.AddAsync(category));
    }
    
    // PUT: api/v1.0/Categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Category category)
    {
        await _categoryService.UpdateAsync(id, category);
        return Ok();
    }
    
    // DELETE: api/v1.0/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.RemoveAsync(id);
        return Ok();
    }
}