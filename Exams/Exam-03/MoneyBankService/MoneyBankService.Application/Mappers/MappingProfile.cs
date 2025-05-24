using MoneyBankService.Domain.Entities;
using AutoMapper;
using MoneyBankService.Application.DTOs;

namespace MoneyBankService.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDTO, Account>();
        CreateMap<Account, AccountDTO>();
        CreateMap<TransactionDTO, Transaction>();
        CreateMap<Transaction, TransactionDTO>();
    }
}