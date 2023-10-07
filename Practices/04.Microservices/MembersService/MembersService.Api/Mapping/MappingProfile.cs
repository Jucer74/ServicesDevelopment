using AutoMapper;
using TeamsService.Api.Dtos;
using TeamsService.Domain.Entities;

namespace MembersService.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Member, Member>();
        CreateMap<Member, Member>();
    }
}
