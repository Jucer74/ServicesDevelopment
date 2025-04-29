using Microsoft.AspNetCore.Mvc;
using Pricat.Application.Interfaces;
using Pricat.Application.DTOs;

namespace Pricat.Api.Controllers;

[ApiController]
[Route("api/v1.0/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _svc;
    public CategoriesController(ICategoryService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await _svc.GetByIdAsync(id));
}