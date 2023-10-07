using AutoMapper;
using MoneyBankService.Api.Dto;
using MoneyBankService.Domain.Entities;
using System.Transactions;
using Transaction = MoneyBankService.Domain.Entities.Transaction;

namespace MoneyBankService.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountDto>();
        CreateMap<TransactionDto, Transaction>();
        CreateMap<Transaction, TransactionDto>();

        CreateMap<TransactionDto, Account>()
            .ForMember(acc => acc.Id, opt => opt.MapFrom(trx => trx.Id))
            .ForMember(acc => acc.AccountNumber, opt => opt.MapFrom(trx => trx.AccountNumber));
        
    }
}

