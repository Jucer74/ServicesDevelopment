using AutoMapper;
using MoneyBankService.Application.Dto;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<Transaction, TransactionDto>().ReverseMap();
        // TODO: Implement de Mapping ForMembers
        //CreateMap<TransactionDto, Account>()
        //    .ForMember(acc => acc.Id, opt => opt.MapFrom(trx => trx.Id));
        //    .ForMember( ....... Oher Fields

    }
}
