using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;

namespace Pricat.Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    // GET: api/v1.0/Products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _productService.GetAllAsync());
    }
    
    // GET: api/v1.0/Products/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _productService.GetByIdAsync(id));
    }
    
    // GET: api/v1.0/Category/{categoryId}/Products
    [HttpGet("/api/v1.0/Category/{categoryId}/Products")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        return Ok(await _productService.GetProductsByCategoryIdAsync(categoryId));
    }
    
    // POST: api/v1.0/Products
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        return Ok(await _productService.AddAsync(product));
    }
    
    // PUT: api/v1.0/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product product)
    {
        await _productService.UpdateAsync(id, product);
        return Ok();
    }
    
    // DELETE: api/v1.0/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.RemoveAsync(id);
        return Ok();
    }
}