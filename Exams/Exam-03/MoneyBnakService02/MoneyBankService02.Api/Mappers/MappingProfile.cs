using AutoMapper;
using MoneyBankService02.Application.Dto;
using MoneyBankService02.Domain.Entities;

namespace MoneyBankService02.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountDto, BankAccount>()
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerName))
            .ForMember(dest => dest.BalanceAmount, opt => opt.MapFrom(src => src.BalanceAmount))
            .ReverseMap();
    }
}
