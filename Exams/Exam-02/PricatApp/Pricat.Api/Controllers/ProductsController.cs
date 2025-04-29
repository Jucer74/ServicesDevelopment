using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Pricat.Application.Dtos;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Api.Controllers;

[Route("api/v1.0/Products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    // GET: api/v1.0/Products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(_mapper.Map<List<ProductDto>>(products));
    }

    // GET api/v1.0/Products/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(_mapper.Map<ProductDto>(product));
    }

    // GET api/v1.0/Categories/5/Products
    [HttpGet("~/api/v1.0/Categories/{categoryId}/Products")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        var products = await _productService.GetByCategoryIdAsync(categoryId);
        return Ok(_mapper.Map<List<ProductDto>>(products));
    }

    // POST api/v1.0/Products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        var product = await _productService.CreateAsync(_mapper.Map<Product>(productDto));

        return Ok(_mapper.Map<ProductDto>(product));

    }

    // PUT api/v1.0/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
    {
        var product = await _productService.UpdateAsync(id, _mapper.Map<Product>(productDto));
        return Ok(_mapper.Map<ProductDto>(product));
    }

    // DELETE api/v1.0/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("Category/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategory(categoryId);

        return Ok(_mapper.Map<List<Product>, List<ProductDto>>(products));
    }
}