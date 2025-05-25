using AutoMapper;
using MoneyBankService.Application.Dtos;
using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountDto>();
    }
}
