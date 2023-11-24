using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MembersService.Domain.Dtos;
using MembersService.Domain.Entities;
using MembersService.Application.Interfaces;
using MembersService.Application.Dtos;

namespace MembersService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutoresController: ControllerBase
{
    private readonly IAutorService _autorService;
    private readonly IMapper _mapper;

    public AutoresController(IAutorService MemberService, IMapper mapper)
    {
        _autorService = MemberService;
        _mapper = mapper;
    }

    // GET: api/<AutoresController>
    [HttpGet]
    public async Task<IActionResult> GetAllAutores()
    {
        var members = await _autorService.GetAllAsync();
        return Ok(_mapper.Map<List<Autor>, List<AutorDTO>>((List<Autor>)members));
    }

    // GET api/<AutoresController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAutorById(int id)
    {
        var Autor = await _autorService.GetByIdAsync(id);
        return Ok(_mapper.Map<Autor, AutorDTO>(Autor));
    }

    //GET api/<AutoresController>/team/5
    [HttpGet("team/{id}")]
    public async Task<IActionResult> GetAutoresByTeamId(int id)
    {
        var Autores = await _autorService.FindAsync(autor => autor.LibroId == id);
        return Ok(_mapper.Map<List<Autor>, List<AutorDTO>>((List<Autor>)Autores));
    }

    // POST api/<AutoresController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]AutorDTO autorDto)
    {
        Autor Autor = await _autorService.AddAsync(_mapper.Map<AutorDTO, Autor>(autorDto));

        return Ok(_mapper.Map<Autor,AutorDTO>(Autor));
    }

    // PUT api/<AutoresController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]AutorDTO autorDto)
    {
        Autor Autor = await _autorService.UpdateAsync(id, _mapper.Map<AutorDTO, Autor>(autorDto));

        return Ok(_mapper.Map<Autor,AutorDTO>(Autor));
    }

    // DELETE api/<AutoresController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _autorService.RemoveAsync(id);
        return Ok();
    }

    // DELETE api/<AutoresController>/libro/5
    [HttpDelete("libro/{id}")]
    public async Task<IActionResult> DeleteByLibroId(int id)
    {
        await _autorService.RemoveMembersByTeamId(autor => autor.LibroId == id, id);
        return Ok();
    }
}
