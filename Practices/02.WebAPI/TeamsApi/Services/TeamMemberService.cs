using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Models;

public class TeamMemberService : ITeamMemberService
{
    private readonly AppDbContext _appDbContext;
    public TeamMemberService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<TeamMember> CreateTeamMember(TeamMember teamMember)
    {
        _appDbContext.TeamMembers.Add(teamMember);
        await _appDbContext.SaveChangesAsync();
        return teamMember;


    }

    public async Task DeleteTeamMember(int id)
    {
        var teamMember = await  _appDbContext.TeamMembers.FindAsync(id);
        if (teamMember != null)
        {
             _appDbContext.TeamMembers.Remove(teamMember);
            await  _appDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<TeamMember>> GetAllTeamMembers()
    {
        return await  _appDbContext.TeamMembers.ToListAsync();
    }

    public async Task<TeamMember> GetTeamMemberById(int id)
    {
        return await  _appDbContext.TeamMembers.FindAsync(id);
    }

    public async Task<TeamMember> UpdateTeamMember(TeamMember teamMember)
    {
         _appDbContext.TeamMembers.Update(teamMember);
        await  _appDbContext.SaveChangesAsync();
        return teamMember;
    }
}

