using TeamsApi.Models;

namespace TeamsApi.Services;

public interface ITeamService
{
    // CreateTeam
    Task<Team> CreateTeam(Team team);

    // DeleteTeam
    Task DeleteTeam(int id);

    // GetAllTeams
    Task<List<Team>> GetAllTeams();

    // GetTeamById
    Task<Team> GetTeamById(int id);

    // UpdateTeam
    Task<Team> UpdateTeam(Team team);
}