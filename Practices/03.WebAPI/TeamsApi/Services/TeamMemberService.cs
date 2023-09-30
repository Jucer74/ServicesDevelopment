<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Services/TeamMemberService.cs
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Models;

public class TeamMemberService : ITeamMemberService
{
    private readonly AppDbContext _appDbContext;
=======
﻿using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Exceptions;
using TeamsApi.Models;

namespace TeamsApi.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly AppDbContext _appDbContext;

>>>>>>> main:Practices/03.WebAPI/TeamsApi/Services/TeamMemberService.cs
    public TeamMemberService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public async Task<TeamMember> CreateTeamMember(TeamMember teamMember)
    {
<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Services/TeamMemberService.cs
        _appDbContext.TeamMembers.Add(teamMember);
        await _appDbContext.SaveChangesAsync();
        return teamMember;


=======
        _appDbContext.Set<TeamMember>().Add(teamMember);
        await _appDbContext.SaveChangesAsync();
        return teamMember;
>>>>>>> main:Practices/03.WebAPI/TeamsApi/Services/TeamMemberService.cs
    }

    public async Task DeleteTeamMember(int id)
    {
<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Services/TeamMemberService.cs
        var teamMember = await  _appDbContext.TeamMembers.FindAsync(id);
        if (teamMember != null)
        {
             _appDbContext.TeamMembers.Remove(teamMember);
            await  _appDbContext.SaveChangesAsync();
        }
=======
        var original = await _appDbContext.Set<TeamMember>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        _appDbContext.Set<TeamMember>().Remove(original);
        await _appDbContext.SaveChangesAsync();
>>>>>>> main:Practices/03.WebAPI/TeamsApi/Services/TeamMemberService.cs
    }

    public async Task<List<TeamMember>> GetAllTeamMembers()
    {
<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Services/TeamMemberService.cs
        return await  _appDbContext.TeamMembers.ToListAsync();
=======
        return await _appDbContext.Set<TeamMember>().ToListAsync<TeamMember>();
>>>>>>> main:Practices/03.WebAPI/TeamsApi/Services/TeamMemberService.cs
    }

    public async Task<TeamMember> GetTeamMemberById(int id)
    {
<<<<<<< HEAD:Practices/02.WebAPI/TeamsApi/Services/TeamMemberService.cs
        return await  _appDbContext.TeamMembers.FindAsync(id);
    }

    public async Task<TeamMember> UpdateTeamMember(TeamMember teamMember)
    {
         _appDbContext.TeamMembers.Update(teamMember);
        await  _appDbContext.SaveChangesAsync();
        return teamMember;
    }
}

=======
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
>>>>>>> main:Practices/03.WebAPI/TeamsApi/Services/TeamMemberService.cs
