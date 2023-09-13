using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamsApi.Dtos;
using TeamsApi.Models;
using TeamsApi.Services;

namespace TeamsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamMembersController:ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly IMapper _mapper;

    public TeamMembersController(ITeamMemberService teamMemberService, IMapper mapper)
    {
        _teamMemberService = teamMemberService;
        _mapper = mapper;
    }

    // GET: api/<TeamMembersController>
    [HttpGet]
    public async Task<IActionResult> GetAllTeamMembers()
    {
        var teamMembers = await _teamMemberService.GetAllTeamMembers();
        return Ok(_mapper.Map<List<TeamMember>, List<TeamMemberDto>>(teamMembers));
    }


    // GET api/<TeamMembersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamMemberById(int id)
    {
        var teamMember = await _teamMemberService.GetTeamMemberById(id);
        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
    }

    // POST api/<TeamMembersController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TeamMemberDto teamMemberDto)
    {
        TeamMember teamMember = await _teamMemberService.CreateTeamMember(_mapper.Map<TeamMemberDto, TeamMember>(teamMemberDto));

        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
    }

    // PUT api/<TeamMembersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TeamMemberDto teamMemberDto)
    {
        teamMemberDto.Id = id;

        TeamMember teamMember = await _teamMemberService.UpdateTeamMember(_mapper.Map<TeamMemberDto, TeamMember>(teamMemberDto));

        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
    }

    // DELETE api/<TeamMembersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teamMemberService.DeleteTeamMember(id);
        return Ok();
    }
}
