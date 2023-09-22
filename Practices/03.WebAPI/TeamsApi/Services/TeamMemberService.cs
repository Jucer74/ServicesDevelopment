using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Exceptions;
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
        _appDbContext.Set<TeamMember>().Add(teamMember);
        await _appDbContext.SaveChangesAsync();
        return teamMember;
    }

    public async Task DeleteTeamMember(int id)
    {
        var original = await _appDbContext.Set<TeamMember>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        _appDbContext.Set<TeamMember>().Remove(original);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<TeamMember>> GetAllTeamMembers()
    {
        return await _appDbContext.Set<TeamMember>().ToListAsync<TeamMember>();
    }

    public async Task<TeamMember> GetTeamMemberById(int id)
    {
        var teamMember = await _appDbContext.Set<TeamMember>().FindAsync(id);
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

        var original = await _appDbContext.Set<TeamMember>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        _appDbContext.Entry(original).CurrentValues.SetValues(teamMember!);
        await _appDbContext.SaveChangesAsync();

        return teamMember!;
    }
}
