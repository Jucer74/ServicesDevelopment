using AutoMapper;
using MoneyBankService.Application.Dtos;
using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountDto>();
    }
}