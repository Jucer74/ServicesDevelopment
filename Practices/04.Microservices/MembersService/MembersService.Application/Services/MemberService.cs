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

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly RestClient _restClient;

    public MemberService(IMemberRepository memberRepository, IConfiguration configuration)
    {
        string baseUrl = configuration["MicroserviceSettings:TeamsServiceUrl"]!;
        _restClient = new RestClient(baseUrl);
        _memberRepository = memberRepository;
        
    }

    public async Task<Member> AddAsync(Member entity)
    {
        int id = entity.TeamId;
        RestRequest restRequest = new($"{id}", Method.Get);
        var restResponse = await _restClient.ExecuteAsync<TeamDto>(restRequest);


        if (!restResponse.IsSuccessful)
        {
            throw restResponse.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundException($"Team with id {id} not found"),
                _ => new InternalServerErrorException("Something went wrong"),
            };
        }

        return await _memberRepository.AddAsync(entity);
    }

    public async Task<IEnumerable<Member>> FindAsync(Expression<Func<Member, bool>> predicate)
    {
        return await _memberRepository.FindAsync(predicate);
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _memberRepository.GetAllAsync();
    }

    public async Task<Member> GetByIdAsync(int id)
    {
        Member member = await _memberRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Team Member with Id={id} Not Found");
        return member;
    }

    public async Task RemoveAsync(int id)
    {
        Member member = await _memberRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Team Member with Id={id} Not Found");
        await _memberRepository.RemoveAsync(member);
    }

    public async Task<Member> UpdateAsync(int id, Member entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to TeamMember.Id [{entity.Id}]");
        }

        _ = await _memberRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Team Member with Id={id} Not Found");

        return (await _memberRepository.UpdateAsync(entity));
    }

    public async Task RemoveMembersByTeamId(Expression<Func<Member, bool>> predicate, int id)
    {
        var members = await _memberRepository.FindAsync(predicate);

        if (!members.Any())
        {
            throw new NotFoundException($"No members found with the TeamId {id}");
        }

        await _memberRepository.RemoveAsync(predicate, id);   
    }
}