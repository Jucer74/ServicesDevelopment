using MembersService.Application.Interfaces;
using MembersService.Domain.Dtos;
using MembersService.Domain.Entities;
using MembersService.Domain.Exceptions;
using MembersService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Linq.Expressions;
using System.Net;

namespace MembersService.Application.Services;

public class AutorService : IAutorService
{
    private readonly IAutorRepository _autorRepository;
    private readonly RestClient _restClient;

    public AutorService(IAutorRepository autorRepository, IConfiguration configuration)
    {
        string baseUrl = configuration["MicroserviceSettings:TeamsServiceUrl"]!;
        _restClient = new RestClient(baseUrl);
        _autorRepository = autorRepository;
        
    }

    public async Task<Autor> AddAsync(Autor entity)
    {
        //int id = entity.LibroId;
        //RestRequest restRequest = new($"{id}", Method.Get);
        //var restResponse = await _restClient.ExecuteAsync<TeamDto>(restRequest);


        /*if (!restResponse.IsSuccessful)
        {
            throw restResponse.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundException($"Team with id {id} not found"),
                _ => new InternalServerErrorException("Something went wrong"),
            };
        }*/

        return await _autorRepository.AddAsync(entity);
    }

    public async Task<IEnumerable<Autor>> FindAsync(Expression<Func<Autor, bool>> predicate)
    {
        return await _autorRepository.FindAsync(predicate);
    }

    public async Task<IEnumerable<Autor>> GetAllAsync()
    {
        return await _autorRepository.GetAllAsync();
    }

    public async Task<Autor> GetByIdAsync(int id)
    {
        Autor autor = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Team Autor with Id={id} Not Found");
        return autor;
    }

    public async Task RemoveAsync(int id)
    {
        Autor autor = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Team Autor with Id={id} Not Found");
        await _autorRepository.RemoveAsync(autor);
    }

    public async Task<Autor> UpdateAsync(int id, Autor entity)
    {
        /*if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to TeamMember.Id [{entity.Id}]");
        }

        _ = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Team Autor with Id={id} Not Found");

        */

        return (await _autorRepository.UpdateAsync(entity));
    }

    public async Task RemoveMembersByTeamId(Expression<Func<Autor, bool>> predicate, int id)
    {
        var members = await _autorRepository.FindAsync(predicate);

        if (!members.Any())
        {
            return;
        }

        await _autorRepository.RemoveAsync(predicate, id);   
    }
}