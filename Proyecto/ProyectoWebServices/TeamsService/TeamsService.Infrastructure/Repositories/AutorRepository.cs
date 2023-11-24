using TeamsService.Domain.Dtos;
using TeamsService.Domain.Interfaces.Repositories;
using RestSharp;
using Microsoft.Extensions.Configuration;
using System.Net;
using TeamsService.Domain.Exceptions;

namespace TeamsService.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly RestClient _restClient;

        public AutorRepository(IConfiguration configuration)
        {
            string baseUrl = configuration["MicroserviceSettings:MembersServiceUrl"]!;
            _restClient = new RestClient(baseUrl);
        }

        public async Task<IEnumerable<AutorDTO>> GetMembersByTeamId(int teamId)
        {
            RestRequest restRequest = new RestRequest($"team/{teamId}", Method.Get);
            var restResponse = await _restClient.ExecuteAsync<IEnumerable<AutorDTO>>(restRequest);

            if (restResponse.IsSuccessful)
            {
               return restResponse.Data!;
            }

            throw new InternalServerErrorException("Something went wrong");
        }

        public async Task RemoveMembersByTeamId(int teamId)
        {
            RestRequest restRequest = new RestRequest($"team/{teamId}", Method.Delete);
            var restResponse = await _restClient.ExecuteAsync(restRequest);

            if (!restResponse.IsSuccessful)
            {
                throw new InternalServerErrorException("Something went wrong");
            }
        }
    }
}