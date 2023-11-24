using AutoMapper;
using TeamsService.Api.Dtos;
using TeamsServie.Domain.Entities;

namespace TeamsService.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LibroDTO, Libro>();
        CreateMap<Libro, LibroDTO>();
    }
}
