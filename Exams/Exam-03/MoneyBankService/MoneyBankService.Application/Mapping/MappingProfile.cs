using AutoMapper;
using MoneyBankService.Application.Dtos;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountDto>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
