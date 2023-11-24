using AutoMapper;
using MembersService.Application.Dtos;
using MembersService.Domain.Dtos;
using MembersService.Domain.Entities;

namespace MembersService.Api.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<AutorDTO, Autor>();
        CreateMap<Autor, AutorDTO>();
    }
}
