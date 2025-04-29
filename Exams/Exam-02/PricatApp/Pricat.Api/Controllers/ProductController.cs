using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Api.Controllers;

// Define la ruta base para este controlador y lo marca como un controlador API
[Route("api/v1.0/Products")]
[ApiController]
public class ProductsController : ControllerBase
{
    // Inyección de dependencias: servicio de productos y AutoMapper
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    // Método HTTP GET: obtiene todos los productos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync(); // Llama al servicio
        return Ok(_mapper.Map<List<ProductDto>>(products)); // Mapea y retorna lista de DTOs
    }

    // Método HTTP GET por ID: obtiene un producto específico por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id); // Busca el producto
        return Ok(_mapper.Map<ProductDto>(product)); // Mapea y retorna el DTO
    }

    // Método HTTP GET personalizado: obtiene productos por el ID de categoría
    // Nota: usa una ruta absoluta con "~" para sobrescribir la ruta base
    [HttpGet("~/api/v1.0/Categories/{categoryId}/Products")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        var products = await _productService.GetByCategoryIdAsync(categoryId); // Busca por categoría
        return Ok(_mapper.Map<List<ProductDto>>(products)); // Mapea y retorna
    }

    // Método HTTP POST: crea un nuevo producto
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        var product = await _productService.CreateAsync(_mapper.Map<Product>(productDto)); // Mapea y crea
        return Ok(_mapper.Map<ProductDto>(product)); // Retorna el producto creado
    }

    // Método HTTP PUT: actualiza un producto por ID
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
    {
        var product = await _productService.UpdateAsync(id, _mapper.Map<Product>(productDto)); // Actualiza
        return Ok(_mapper.Map<ProductDto>(product)); // Retorna actualizado
    }

    // Método HTTP DELETE: elimina un producto por ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id); // Elimina el producto
        return Ok(); // Retorna 200 OK vacío
    }

    // Método HTTP GET adicional: otra forma de obtener productos por categoría
    // Nota: esta ruta usa "Products/Category/{categoryId}"
    [HttpGet("Category/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategory(categoryId); // Consulta
        return Ok(_mapper.Map<List<Product>, List<ProductDto>>(products)); // Mapea y retorna
    }
}
