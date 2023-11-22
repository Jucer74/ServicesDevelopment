using AutoMapper;
using PizzeriaApi.Dtos;
using PizzeriaApi.Models;

namespace PizzeriaApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PizzeriaDto, Pizzeria>();
        CreateMap<Pizzeria, PizzeriaDto>();
        CreateMap<PizzeriaCategoriaDto, PizzeriaCategoria>();
        CreateMap<PizzeriaCategoria, PizzeriaCategoriaDto>();
    }
}
