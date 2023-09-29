using System.Collections.Generic;
using System.Linq.Expressions;
using TeamsService.Domain.Dtos;

namespace TeamsService.Domain.Interfaces.Repositories;

public interface IMemberRepository
{
    public Task<IEnumerable<TeamMemberDto>> GetMembersByTeamId(int teamId);
    public Task RemoveMembersByTeamId(int teamId);
}