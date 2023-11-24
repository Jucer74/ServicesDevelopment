using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamsService.Api.Dtos;
using TeamsService.Application.Interfaces;

using TeamsServie.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamsService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService _libroService;
        private readonly IMapper _mapper;

        public LibrosController(ILibroService teamService, IMapper mapper)
        {
            _libroService = teamService;
            _mapper = mapper;
        }

        // GET: api/<TeamsController>
        [HttpGet]
        public async Task<IActionResult> GetAllLibros()
        {
            var teams = await _libroService.GetAllTeams() as List<Libro>;
            return Ok(_mapper.Map<List<Libro>, List<LibroDTO>>(teams!));
        }

        // GET api/<TeamsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var libro = await _libroService.GetTeamById(id);
            return Ok(_mapper.Map<Libro, LibroDTO>(libro));
        }

        // POST api/<TeamsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LibroDTO teamDto)
        {
            var libro = await _libroService.CreateTeam(_mapper.Map<LibroDTO, Libro>(teamDto));
            return Ok(_mapper.Map<Libro, LibroDTO>(libro));
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LibroDTO teamDto)
        {
            var libro = await _libroService.UpdateTeam(id, _mapper.Map<LibroDTO, Libro>(teamDto));
            return Ok(_mapper.Map<Libro, LibroDTO>(libro));
        }

        // DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _libroService.DeleteTeam(id);
            return Ok();
        }

        // GET api/<TeamsController>/5/Members
        [HttpGet("{id}/Autores")]
        public async Task<IActionResult> GetAutoresByTeamId(int id)
        {
            var members = await _libroService.GetAutoresByTeamId(id);
            return Ok(members);
        }
    }
}