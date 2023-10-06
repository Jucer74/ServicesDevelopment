using AutoMapper;
using TeamsService.Api.Dtos;
using TeamsService.Domain.Entities;

namespace TeamsService.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TeamDto, Team>();
        CreateMap<Team, TeamDto>();
    }
}
