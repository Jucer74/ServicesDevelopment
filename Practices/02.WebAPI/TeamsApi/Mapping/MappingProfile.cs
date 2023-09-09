using AutoMapper;
using TeamsApi.Dtos;
using TeamsApi.Models;

namespace TeamsApi.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<TeamDto, Team>();
        //mapeo a la inversa CreateMap<Team, TeamDto>();
        CreateMap<TeamMemberDto, TeamMember>();
    }
}
