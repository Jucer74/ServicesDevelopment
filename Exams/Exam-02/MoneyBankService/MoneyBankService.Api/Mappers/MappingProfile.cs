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

        // Mapea TransactionDto a Account y viceversa
        CreateMap<TransactionDto, Account>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.BalanceAmount, opt => opt.MapFrom(src => src.ValueAmount))
            .ForMember(dest => dest.CreationDate, opt => opt.Ignore()) // Ignora la propiedad CreationDate
            .ForMember(dest => dest.AccountType, opt => opt.Ignore()) // Ignora la propiedad AccountType
            .ForMember(dest => dest.OwnerName, opt => opt.Ignore()) // Ignora la propiedad OwnerName
            .ForMember(dest => dest.OverdraftAmount, opt => opt.Ignore()); // Ignora la propiedad OverdraftAmount

        CreateMap<Account, TransactionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.ValueAmount, opt => opt.MapFrom(src => src.BalanceAmount));
    }
}

