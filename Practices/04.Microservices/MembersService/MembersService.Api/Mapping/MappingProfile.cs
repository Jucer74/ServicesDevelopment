using AutoMapper;
using MembersService.Domain.Dtos;
using MembersService.Domain.Entities;

namespace MembersService.Api.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<MemberDto, Member>();
        CreateMap<Member, MemberDto>();
    }
}
