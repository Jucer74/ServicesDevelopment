using AutoMapper;
using TeamsApi.Aplicacion.Dtos;
using TeamsApi.Dominio.Models;

namespace TeamsApi.Aplicacion.Mapping;

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