using AutoMapper;
using PizzeriaApi.Dtos;
using PizzeriaApi.Models;
using PizzeriaApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzeriaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzeriaCategoriaController : ControllerBase
{
    private readonly IPizzeriaCategoriaService _PizzeriaCategoriaService;
    private readonly IMapper _mapper;

    public PizzeriaCategoriaController(IPizzeriaCategoriaService PizzeriaCategoriaService, IMapper mapper)
    {
        _PizzeriaCategoriaService = PizzeriaCategoriaService;
        _mapper = mapper;
    }

    // GET: api/<PizzeriaCategoriaController>
    [HttpGet]
    public async Task<IActionResult> GetAllPizzeriaCategorias()
    {
        var PizzeriaCategorias = await _PizzeriaCategoriaService.GetAllPizzeriaCategorias();
        return Ok(_mapper.Map<List<PizzeriaCategoria>, List<PizzeriaCategoriaDto>>(PizzeriaCategorias));
    }

    // GET api/<PizzeriaCategoriaController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPizzeriaCategoriaById(int id)
    {
        var PizzeriaCategoria = await _PizzeriaCategoriaService.GetPizzeriaCategoriaById(id);
        return Ok(_mapper.Map<PizzeriaCategoria, PizzeriaCategoriaDto>(PizzeriaCategoria));
    }

    // POST api/<PizzeriaCategoriaController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PizzeriaCategoriaDto PizzeriaCategoriaDto)
    {
        var PizzeriaCategoria = await _PizzeriaCategoriaService.CreatePizzeriaCategoria(_mapper.Map<PizzeriaCategoriaDto, PizzeriaCategoria>(PizzeriaCategoriaDto));
        return Ok(_mapper.Map<PizzeriaCategoria, PizzeriaCategoriaDto>(PizzeriaCategoria));
    }

    // PUT api/<PizzeriaCategoriaController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PizzeriaCategoriaDto PizzeriaCategoriaDto)
    {
        var PizzeriaCategoria = await _PizzeriaCategoriaService.UpdatePizzeriaCategoria(id, _mapper.Map<PizzeriaCategoriaDto, PizzeriaCategoria>(PizzeriaCategoriaDto));
        return Ok(_mapper.Map<PizzeriaCategoria, PizzeriaCategoriaDto>(PizzeriaCategoria));
    }

    // DELETE api/<Pizzeria CategoriaController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _PizzeriaCategoriaService.DeletePizzeriaCategoria(id);
        return Ok();
    }
}
