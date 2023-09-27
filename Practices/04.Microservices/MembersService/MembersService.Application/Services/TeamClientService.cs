using MembersService.Application.Interfaces;
using MembersService.Domain.Dtos;
using MembersService.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;

namespace Members.Application.Services;

public class TeamClientService: ITeamClientService,IDisposable
{
    private readonly RestClient _restClient;

    public TeamClientService(IConfiguration configuration)
    {
        string baseUrl = configuration["MicroserviceSettings:TeamsServiceUrl"]!;
        _restClient = new RestClient(baseUrl);
    }

    public void Dispose()
    {
        _restClient.Dispose();
    }

    public async Task<TeamDto> GetByIdAsync(int id)
    {
        var request = new RestRequest($"{id}", Method.Get);
        var response = await _restClient.ExecuteAsync<TeamDto>(request);

        if (response.IsSuccessful)
        {
            return response.Data;
        }
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                throw new NotFoundException($"Team with id {id} not found");
            default:
                throw new InternalServerErrorException("Something went wrong");
        }
    }
}
