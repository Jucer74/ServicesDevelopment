using AutoMapper;
using MembersService.Api.Dtos;
using MembersService.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MembersService.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MemberDto, Member>();
        CreateMap<Member, MemberDto>();
    }
}
