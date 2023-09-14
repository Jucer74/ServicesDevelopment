using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamsApi.Dtos;
using TeamsApi.Models;
using TeamsApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;
        private readonly IMapper _mapper;

        public MembersController(ITeamMemberService teamMemberService, IMapper mapper)
        {
            _teamMemberService = teamMemberService;
            _mapper = mapper;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public async Task<IActionResult> GetAllTeamMembers()
        {
            var teamMembers = await _teamMemberService.GetAllTeamMembers();
            return Ok(_mapper.Map<List<TeamMember>, List<TeamMemberDto>>(teamMembers));
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMemberById(int id)
        {
            var teamMember = await _teamMemberService.GetTeamMemberById(id);
            return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
        }

        // POST api/<MembersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeamMemberDto teamMemberDto)
        {
            var teamMember = await _teamMemberService.CreateTeamMember(_mapper.Map<TeamMemberDto, TeamMember>(teamMemberDto));
            return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TeamMemberDto teamMemberDto)
        {
            var teamMember = await _teamMemberService.UpdateTeamMember(id, _mapper.Map<TeamMemberDto, TeamMember>(teamMemberDto));
            return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamMemberService.DeleteTeamMember(id);
            return Ok();
        }
    }
}
