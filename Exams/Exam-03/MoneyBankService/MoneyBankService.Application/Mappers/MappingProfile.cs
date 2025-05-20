using MoneyBankService.Application.Dto;
using MoneyBankService.Domain.Entities;
using AutoMapper;

namespace MoneyBankService.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountDto>();
        CreateMap<TransactionDto, Transaction>();
        CreateMap<Transaction, TransactionDto>();
    }
}