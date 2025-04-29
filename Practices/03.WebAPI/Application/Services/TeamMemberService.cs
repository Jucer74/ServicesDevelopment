using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Domain.Models;
using Application.Exceptions;

namespace Application.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ITeamRepository _teamRepository;

    public TeamMemberService(
        ITeamMemberRepository teamMemberRepository,
        ITeamRepository teamRepository)
    {
        _teamMemberRepository = teamMemberRepository;
        _teamRepository = teamRepository;
    }

    public async Task<TeamMember> CreateTeamMember(TeamMember teamMember)
    {
        // Verificar si el equipo existe
        var team = await _teamRepository.GetByIdAsync(teamMember.TeamId);
        if (team is null)
        {
            throw new NotFoundException($"Team with Id={teamMember.TeamId} Not Found");
        }

        return await _teamMemberRepository.AddAsync(teamMember);
    }

    public async Task DeleteTeamMember(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        if (teamMember is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        await _teamMemberRepository.RemoveAsync(teamMember);
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
        return teamMember;
    }

    public async Task<TeamMember> UpdateTeamMember(int id, TeamMember teamMember)
    {
        if (id != teamMember.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to TeamMember.Id [{teamMember.Id}]");
        }

        var existingMember = await _teamMemberRepository.GetByIdAsync(id);
        if (existingMember is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        // Verificar si el equipo existe si se cambió el TeamId
        if (existingMember.TeamId != teamMember.TeamId)
        {
            var team = await _teamRepository.GetByIdAsync(teamMember.TeamId);
            if (team is null)
            {
                throw new NotFoundException($"Team with Id={teamMember.TeamId} Not Found");
            }
        }

        return await _teamMemberRepository.UpdateAsync(teamMember);
    }
}