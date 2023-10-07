using TeamsService.Domain.Dtos;
using TeamsService.Domain.Entities;

namespace MembersService.Application.Interfaces;

public interface IMemberService
{
    Task<Team> CreateTeam(Team team);

    Task DeleteTeam(int id);

    Task<IEnumerable<Team>> GetAllTeams();

    Task<Team> GetTeamById(int id);

    Task<Team> UpdateTeam(int id, Team team);

    Task<IEnumerable<TeamMemberDto>> GetTeamMembersByTeamId(int id);
}