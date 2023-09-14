using TeamsApi.Models;

namespace TeamsApi.Services;

public interface ITeamMemberService
{
    Task<TeamMember> CreateTeamMember(TeamMember teamMember);

    Task<TeamMember> UpdateTeamMember(TeamMember teamMember);

    Task DeleteTeamMember(int id);

    Task<List<TeamMember>> GetAllTeamMembers();

    Task<TeamMember> GetTeamMemberById(int id);

    Task<Team> GetTeam(int id);
}
