using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;
using Pricat.Application.Exceptions;

namespace Pricat.Api.Controllers;

[Route("api/v1.0/Categories")]
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

    // GET: api/v1.0/Categories
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(_mapper.Map<List<CategoryDto>>(categories));
    }

    // GET api/v1.0/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    // POST api/v1.0/Categories
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
    {
        var category = await _categoryService.CreateAsync(_mapper.Map<Category>(categoryDto));
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    // PUT api/v1.0/Categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto)
    {
        var category = await _categoryService.UpdateAsync(id, _mapper.Map<Category>(categoryDto));
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    // DELETE api/v1.0/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok();
    }
}