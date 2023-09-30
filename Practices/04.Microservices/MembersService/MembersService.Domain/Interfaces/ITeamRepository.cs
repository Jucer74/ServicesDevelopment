using MembersService.Domain.Dtos;

namespace MembersService.Domain.Interfaces;

public interface ITeamRepository
{
    public Task<TeamDto> GetByIdAsync(int id);
}
