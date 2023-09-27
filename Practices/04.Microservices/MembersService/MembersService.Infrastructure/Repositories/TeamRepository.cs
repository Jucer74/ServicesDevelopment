using MembersService.Domain.Dtos;
using MembersService.Domain.Exceptions;
using MembersService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;

namespace MembersService.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly RestClient _restClient;

    public TeamRepository(IConfiguration configuration)
    {
        string baseUrl = configuration["MicroserviceSettings:TeamsServiceUrl"]!;
        _restClient = new RestClient(baseUrl);
    }
    public async Task<TeamDto> GetByIdAsync(int id)
    {
        RestRequest restRequest = new($"{id}", Method.Get);
        var restResponse = await _restClient.ExecuteAsync<TeamDto>(restRequest);

        if (restResponse.IsSuccessful)
        {
            return restResponse.Data!;
        }

        throw restResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => new NotFoundException($"Team with id {id} not found"),
            _ => new InternalServerErrorException("Something went wrong"),
        };

    }
}
