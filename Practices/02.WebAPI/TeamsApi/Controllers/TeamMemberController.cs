using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamsApi.Dtos;
using TeamsApi.Models;
using TeamsApi.Services;

[Route("api/[controller]")]
[ApiController]
public class TeamMembersController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly IMapper _mapper;

    public TeamMembersController(ITeamMemberService teamMemberService, IMapper mapper)
    {
        _teamMemberService = teamMemberService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeamMembers()
    {
        var teamMembers = await _teamMemberService.GetAllTeamMembers();
        return Ok(_mapper.Map<List<TeamMember>, List<TeamMemberDto>>(teamMembers));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamMemberById(int id)
    {
        var teamMember = await _teamMemberService.GetTeamMemberById(id);
        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TeamMemberDto teamMember)
    {
        return Ok(await _teamMemberService.CreateTeamMember(_mapper.Map<TeamMemberDto, TeamMember>(teamMember)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TeamMemberDto teamMember)
    {
        return Ok(await _teamMemberService.UpdateTeamMember(_mapper.Map<TeamMemberDto, TeamMember>(teamMember)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teamMemberService.DeleteTeamMember(id);
        return NoContent(); // 204 No Content
    }
}
