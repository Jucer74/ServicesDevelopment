// Application/Services/TeamService.cs
using Domain.Models;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;

namespace Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamService(
        ITeamRepository teamRepository,
        ITeamMemberRepository teamMemberRepository)
    {
        _teamRepository = teamRepository;
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<Team> CreateTeam(Team team)
    {
        return await _teamRepository.AddAsync(team);
    }

    public async Task DeleteTeam(int id)
    {
        var team = await _teamRepository.GetByIdAsync(id);
        if (team is null)
        {
            throw new NotFoundException($"Team with Id={id} Not Found");
        }

        await _teamRepository.RemoveAsync(team);
    }

    public async Task<List<Team>> GetAllTeams()
    {
        return (await _teamRepository.GetAllAsync()).ToList();
    }

    public async Task<Team> GetTeamById(int id)
    {
        var team = await _teamRepository.GetByIdAsync(id);
        if (team is null)
        {
            throw new NotFoundException($"Team with Id={id} Not Found");
        }
        return team;
    }

    public async Task<List<TeamMember>> GetTeamMembersByTeamId(int id)
    {
        var teamMembers = await _teamRepository.GetTeamByIdIncludeTeamMember(id);
        return teamMembers!.Members;
    }

    public async Task<Team> UpdateTeam(int id, Team team)
    {
        if (id != team.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Team.Id [{team.Id}]");
        }

        var existingTeam = await _teamRepository.GetByIdAsync(id);
        if (existingTeam is null)
        {
            throw new NotFoundException($"Team with Id={id} Not Found");
        }

        return await _teamRepository.UpdateAsync(team);
    }
}