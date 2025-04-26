using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Domain.Models;

namespace Application.Services
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
            await _teamRepository.AddAsync(team);
            return team;
        }

        public async Task DeleteTeam(int id)
        {
            var original = await _teamRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Team with Id={id} Not Found");
            }

            await _teamRepository.RemoveAsync(original);   
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return (await _teamRepository.GetAllAsync()).ToList();
        }

        public async Task<Team> GetTeamById(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team is null)
            {
                throw new NotFoundException($"Team with Id={id} Not Found");
            }
            return team!;
        }

        public async Task<List<TeamMember>> GetTeamMembersByTeamId(int id)
        {
            var teamMembers = await _teamRepository.GetTeamByIdIncludeTeamMember(id);
            return teamMembers!.Members;
        }

        public async Task<Team> UpdateTeam(int id, Team team)
        {
            if(id != team.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Team.Id [{team.Id}]");
            }

            var original = await _teamRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Team with Id={id} Not Found");
            }


            await _teamRepository.UpdateAsync(team);

            return team!;       
        }
    }
}
