using MembersService.Domain.Dtos;
namespace MembersService.Application.Interfaces;

public interface ITeamClientService
{
    public Task<TeamDto>GetByIdAsync(int id);
}