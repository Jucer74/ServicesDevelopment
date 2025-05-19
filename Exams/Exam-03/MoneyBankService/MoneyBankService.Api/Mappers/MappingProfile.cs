using AutoMapper;
using MoneyBankService.Application.DTO;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Infrastructure.Repositories;

namespace MoneyBankService.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<AccountCreateDto, Account>().ReverseMap();

        
        CreateMap<Account, AccountResultDto>().ReverseMap();
        
        CreateMap<TransactionCreateDto, Transaction>().ReverseMap();

        
        CreateMap<Transaction, TransactionResultDto>().ReverseMap();

    }
}
