using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Domain.Models;
using Application.Exceptions;
namespace Application.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamMemberService(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }
    public async Task<TeamMember> CreateTeamMember(TeamMember teamMember)
    {
        return await _teamMemberRepository.AddAsync(teamMember);
    }

    public async Task DeleteTeamMember(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        
        if (teamMember == null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        await _teamMemberRepository.RemoveAsync(teamMember);
    }

    public async Task<List<TeamMember>> GetAllTeamMembers()
    {
        return (await _teamMemberRepository.GetAllAsync()).ToList();
    }

    public async Task<TeamMember> GetTeamMemberById(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);

        if (teamMember == null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        return teamMember;
    }

    public async Task<TeamMember> UpdateTeamMember(int id, TeamMember entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to TeamMember.Id [{entity.Id}]");
        }

        var teamMember = await _teamMemberRepository.GetByIdAsync(id);
        if (teamMember is null)
        {
            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }
        return await _teamMemberRepository.UpdateAsync(entity);
    }
}
