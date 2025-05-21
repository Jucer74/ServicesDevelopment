using AutoMapper;
using MoneyBankService.Application.Dto;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, Account>().ReverseMap();
        
    }
}

