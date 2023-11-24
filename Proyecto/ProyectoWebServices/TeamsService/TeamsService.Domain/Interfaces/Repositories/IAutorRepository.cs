using System.Collections.Generic;
using System.Linq.Expressions;
using TeamsService.Domain.Dtos;

namespace TeamsService.Domain.Interfaces.Repositories;

public interface IAutorRepository
{
    public Task<IEnumerable<AutorDTO>> GetMembersByTeamId(int teamId);
    public Task RemoveMembersByTeamId(int teamId);
}