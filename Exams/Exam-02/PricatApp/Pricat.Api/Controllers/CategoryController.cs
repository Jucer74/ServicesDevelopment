using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;
using Pricat.Application.Exceptions;

namespace Pricat.Api.Controllers;

// Ruta base del controlador y configuración como API Controller
[Route("api/v1.0/Categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    // Inyección de dependencias: servicio de categorías y AutoMapper
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    // Método HTTP GET: obtiene todas las categorías
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync(); // Llama al servicio
        return Ok(_mapper.Map<List<CategoryDto>>(categories)); // Mapea a DTO y retorna
    }

    // Método HTTP GET por ID: obtiene una categoría específica
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id); // Busca la categoría por ID
        return Ok(_mapper.Map<CategoryDto>(category)); // Mapea a DTO y retorna
    }

    // Método HTTP POST: crea una nueva categoría
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
    {
        // Mapea de DTO a entidad, crea la categoría y la devuelve como DTO
        var category = await _categoryService.CreateAsync(_mapper.Map<Category>(categoryDto));
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    // Método HTTP PUT: actualiza una categoría existente por ID
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto)
    {
        // Mapea de DTO a entidad, actualiza y retorna como DTO
        var category = await _categoryService.UpdateAsync(id, _mapper.Map<Category>(categoryDto));
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    // Método HTTP DELETE: elimina una categoría por ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id); // Llama al servicio para eliminar
        return Ok(); // Retorna 200 OK sin contenido adicional
    }
}
