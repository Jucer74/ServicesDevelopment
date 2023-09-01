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
        
        // Add the Other Mappings
        // Product <-> ProductDto
        // Order <-> OrderDto
        // OrderDetail <-> OrderDetailDto
        // Customer -> CustomerOrderDto
        //              (Mebers Customer -> CustomerDto)
        //              (Mebers Orders -> OrdersDto)
        // OrderDetail ->  OrderDetailProductDto
        // Order -> OrderOrderDetailDto>

    }
}