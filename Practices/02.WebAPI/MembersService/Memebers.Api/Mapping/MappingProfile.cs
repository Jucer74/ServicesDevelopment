using AutoMapper;
using Members.Domain.Dtos;
using Members.Domain.Entities;

namespace Members.Api.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<MemberDto, Member>();
        CreateMap<Member, MemberDto>();
    }
}
