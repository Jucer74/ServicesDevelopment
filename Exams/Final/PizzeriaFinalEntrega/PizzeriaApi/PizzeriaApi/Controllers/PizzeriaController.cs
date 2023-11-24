using AutoMapper;
using PizzeriaApi.Dtos;
using PizzeriaApi.Models;
using PizzeriaApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzeriaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzeriasController : ControllerBase
{
    private readonly IPizzeriaService _PizzeriaService;
    private readonly IMapper _mapper;

    public PizzeriasController(IPizzeriaService PizzeriaService, IMapper mapper)
    {
        _PizzeriaService = PizzeriaService;
        _mapper = mapper;
    }

    // GET: api/<PizzeriasController>
    [HttpGet]
    public async Task<IActionResult> GetAllPizzerias()
    {
        var Pizzerias = await _PizzeriaService.GetAllPizzerias();
        return Ok(_mapper.Map<List<Pizzeria>, List<PizzeriaDto>>(Pizzerias));
    }

    // GET api/<PizzeriasController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPizzeriaById(int id)
    {
        var Pizzeria = await _PizzeriaService.GetPizzeriaById(id);
        return Ok(_mapper.Map<Pizzeria, PizzeriaDto>(Pizzeria));
    }

    // POST api/<PizzeriasController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PizzeriaDto PizzeriaDto)
    {
        var Pizzeria = await _PizzeriaService.CreatePizzeria(_mapper.Map<PizzeriaDto, Pizzeria>(PizzeriaDto));
        return Ok(_mapper.Map<Pizzeria, PizzeriaDto>(Pizzeria));
    }

    // PUT api/<PizzeriasController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PizzeriaDto PizzeriaDto)
    {
        var Pizzeria = await _PizzeriaService.UpdatePizzeria(id, _mapper.Map<PizzeriaDto, Pizzeria>(PizzeriaDto));
        return Ok(_mapper.Map<Pizzeria, PizzeriaDto>(Pizzeria));
    }

    // DELETE api/<PizzeriasController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _PizzeriaService.DeletePizzeria(id);
        return Ok();
    }

    // GET api/<PizzeriasController>/5/Category
    [HttpGet("{id}/Category")]
    public async Task<IActionResult> GetCategoriaByPizzeriaId(int id)
    {
        var Categoria = await _PizzeriaService.GetPizzeriaCategoriaByPizzeriaId(id);
        return Ok(_mapper.Map<List<PizzeriaCategoria>, List<PizzeriaCategoriaDto>>(Categoria));
    }
}
