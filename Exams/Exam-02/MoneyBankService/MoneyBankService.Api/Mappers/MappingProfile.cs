using AutoMapper;
using MoneyBankService.Api.Dto;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Accounts
        CreateMap<AccountDto, Accounts>();
        CreateMap<Accounts, AccountDto>();

        // Transactions
        CreateMap<TransactionDto, Transactions>();
        CreateMap<Transactions, TransactionDto>();
    }
}