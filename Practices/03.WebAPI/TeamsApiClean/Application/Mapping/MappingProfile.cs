using Application.Dtos;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<TeamDto, Team>();
        CreateMap<Team, TeamDto>();
        CreateMap<TeamMemberDto, TeamMember>();
        CreateMap<TeamMember, TeamMemberDto>();
    }
}
