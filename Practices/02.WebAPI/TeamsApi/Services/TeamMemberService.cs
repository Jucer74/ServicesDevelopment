using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Models;

namespace TeamsApi.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly AppDbContext _appDbContext;
    public TeamMemberService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<TeamMember> CreateTeamMember(TeamMember teamMember)
    {
        var teamId = teamMember?.TeamId;
        Team team = await _appDbContext.Set<Team>().FindAsync(teamId);

        if (team is null)
        {
            throw new Exception($"Team with Id={teamId} Not Found");
        }

        _appDbContext.Set<TeamMember>().Add(teamMember);
        await _appDbContext.SaveChangesAsync();
        return teamMember;
    }

    public async Task DeleteTeamMember(int id)
    {
        var original = await _appDbContext.Set<TeamMember>().FindAsync(id) ?? throw new Exception($"TeamMember with Id={id} Not Found");

        _appDbContext.Set<TeamMember>().Remove(original);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<TeamMember>> GetAllTeamMembers()
    {
        return await _appDbContext.Set<TeamMember>().ToListAsync<TeamMember>();
    }

    public async Task<TeamMember> GetTeamMemberById(int id)
    {
        return await _appDbContext.Set<TeamMember>().FindAsync(id) ?? throw new Exception($"TeamMember with Id={id} Not Found");
    }

    public async Task<Team>GetTeam(int id)
    {
        TeamMember teamMember = await _appDbContext.Set<TeamMember>().FindAsync(id) ?? throw new Exception($"TeamMember with Id={id} Not Found");

        Team team = await _appDbContext.Set<Team>().FindAsync(teamMember.TeamId) ?? throw new Exception($"Team with Id={teamMember.TeamId} Not Found");

        return team;
    }


    public async Task<TeamMember> UpdateTeamMember(int id,TeamMember teamMember)
    {
       if (id != teamMember.Id)
        {
            throw new Exception($"TeamMenber id different than {id}");
        }

        var original = await _appDbContext.Set<TeamMember>().FindAsync(id) ?? throw new Exception($"TeamMember with Id={id} Not Found");

        _appDbContext.Entry(original).CurrentValues.SetValues(teamMember);
        await _appDbContext.SaveChangesAsync();
        return teamMember;
    }
}
