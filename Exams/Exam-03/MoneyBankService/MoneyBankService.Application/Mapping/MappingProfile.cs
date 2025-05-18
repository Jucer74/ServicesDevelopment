using AutoMapper;
using MoneyBankService.Application.Dtos;
using MoneyBankService.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoneyBankService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountDto>();
    }
}