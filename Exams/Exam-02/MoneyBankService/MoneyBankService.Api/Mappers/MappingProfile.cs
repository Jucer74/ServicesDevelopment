using AutoMapper;
using MoneyBankService.Api.Dto;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountDto>();
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionDto,Transaction>();
        CreateMap<TransactionDto, Account>()
            .ForMember(acc => acc.Id, opt => opt.MapFrom(trx => trx.Id))
            .ForMember(acc => acc.AccountNumber, opt => opt.MapFrom(trx => trx.AccountNumber));
    }
}
