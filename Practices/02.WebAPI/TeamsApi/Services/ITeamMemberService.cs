using System.Collections.Generic;
using System.Threading.Tasks;
using TeamsApi.Models;

public interface ITeamMemberService
{
    Task<TeamMember> CreateTeamMember(TeamMember teamMember);
    Task DeleteTeamMember(int id);
    Task<List<TeamMember>> GetAllTeamMembers();
    Task<TeamMember> GetTeamMemberById(int id);
    Task<TeamMember> UpdateTeamMember(TeamMember teamMember);
}

