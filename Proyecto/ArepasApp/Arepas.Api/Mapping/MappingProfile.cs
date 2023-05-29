using Arepas.Api.Dtos;
using Arepas.Domain.Models;
using AutoMapper;

namespace Arepas.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CustomerDto, Customer>();
        CreateMap<Customer, CustomerDto>();
    }
}