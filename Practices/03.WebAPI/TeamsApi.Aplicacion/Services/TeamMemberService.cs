using TeamsApi.Aplicacion.Exceptions;
using TeamsApi.Dominio.Interfaces.Repositories;
using TeamsApi.Dominio.Models;

namespace TeamsApi.Aplicacion.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamMemberService(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }


    public async Task<TeamMember> CreateTeamMember(TeamMember teamMember)
    {
        await _teamMemberRepository.AddAsync(teamMember);
        return teamMember;
    }

    public async Task DeleteTeamMember(int id)
    {
        var original = await _teamMemberRepository.GetByIdAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        await _teamMemberRepository.RemoveAsync(original);
    }

    public async Task<List<TeamMember>> GetAllTeamMembers()
    {
        return (await _teamMemberRepository.GetAllAsync()).ToList();
    }

    public async Task<TeamMember> GetTeamMemberById(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        if (teamMember is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        return teamMember!;
    }

    public async Task<TeamMember> UpdateTeamMember(int id, TeamMember teamMember)
    {
        if (id != teamMember.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to TeamMember.Id [{teamMember.Id}]");
        }

        var original = await _teamMemberRepository.GetByIdAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        await _teamMemberRepository.UpdateAsync(teamMember);

        return teamMember!;
    }
}
