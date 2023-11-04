using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
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
        private readonly IConfiguration _configuration;
        private readonly RestClient _restClient;

        public TeamService(ITeamRepository teamRepository, IConfiguration configuration, RestClient restClient)
        {
            _teamRepository = teamRepository;
            _configuration = configuration;
            _restClient = restClient;
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
            var membersResponse = await CallMembersService(id);
            var members = JsonConvert.DeserializeObject<IEnumerable<TeamMemberDto>>(membersResponse);
            return members!;
        }

        private async Task<string> CallMembersService(int teamId)
        {
            var membersServiceUrl = _configuration.GetSection("MembersServiceUrl").Value;

            var membersEndPoint = $"{membersServiceUrl}/Team/{teamId}";

            var request = new RestRequest(membersEndPoint);

            var response = await _restClient.GetAsync(request);

            var responseData = response.Content;

            if (!response.IsSuccessful && response.StatusCode != HttpStatusCode.OK)
            {
                throw new InternalServerErrorException($"Error Getting Members By Teamd Id = {teamId}");
            }

            return responseData!;
        }
    }
}