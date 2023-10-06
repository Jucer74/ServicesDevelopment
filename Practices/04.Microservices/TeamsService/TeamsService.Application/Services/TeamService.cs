using TeamsService.Application.Interfaces;
using TeamsService.Domain.Dtos;
using TeamsService.Domain.Entities;
using TeamsService.Domain.Exceptions;
using TeamsService.Domain.Interfaces.Repositories;

namespace TeamsService.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team> CreateTeam(Team team)
        {
            return await _teamRepository.AddAsync(team);
        }

        public async Task DeleteTeam(int id)
        {
            var original = await _teamRepository.GetByIdAsync(id);

            if (original is not null)
            {
                await _teamRepository.RemoveAsync(original);
                return;
            }

            throw new NotFoundException($"Team with Id={id} Not Found");
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _teamRepository.GetAllAsync();
        }

        public async Task<Team> GetTeamById(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team is not null)
            {
                return team;
            }

            throw new NotFoundException($"Team with Id={id} Not Found");
        }

        public async Task<Team> UpdateTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Team.Id [{team.Id}]");
            }

            var original = await _teamRepository.GetByIdAsync(id);

            if (original is not null)
            {
                return await _teamRepository.UpdateAsync(team);
            }

            throw new NotFoundException($"Team with Id={id} Not Found");
        }

        public async Task<IEnumerable<TeamMemberDto>> GetTeamMembersByTeamId(int id)
        {
            // Call the Members Service
            var members = new List<TeamMemberDto>();

            return members;
        }
    }
}