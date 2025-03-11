using TeamsService.Domain.Dtos;
using TeamsService.Domain.Entities;

namespace TeamsService.Application.Interfaces;

public interface ITeamService
{
    Task<Team> CreateTeam(Team team);

    Task DeleteTeam(int id);

    Task<IEnumerable<Team>> GetAllTeams();

    Task<Team> GetTeamById(int id);

    Task<Team> UpdateTeam(int id, Team team);

    Task<IEnumerable<TeamMemberDto>> GetTeamMembersByTeamId(int id);
}