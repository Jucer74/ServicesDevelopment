using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pricat.Application.DTOs;
using Pricat.Application.Interfaces;

namespace Pricat.Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService) => _productService = productService;

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll() => Ok(await _productService.GetAllProducts());

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productService.GetProductById(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpGet("/api/v1.0/Category/{categoryId}/Products")]
    public async Task<ActionResult<List<ProductDto>>> GetByCategoryId(int categoryId)
    {
        return Ok(await _productService.GetByCategoryId(categoryId));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto dto)
    {
        var result = await _productService.CreateProduct(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
    {
        await _productService.UpdateProduct(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProduct(id);
        return Ok();
    }
}
