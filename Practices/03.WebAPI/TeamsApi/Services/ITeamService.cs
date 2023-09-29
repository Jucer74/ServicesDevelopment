using TeamsApi.Models;

namespace TeamsApi.Services;

public interface ITeamService
{
    Task<Team> CreateTeam(Team team);

    Task DeleteTeam(int id);

    Task<List<Team>> GetAllTeams();

    Task<Team> GetTeamById(int id);

    Task<Team> UpdateTeam(int id, Team team);

    Task<List<TeamMember>> GetTeamMembersByTeamId(int id);
}