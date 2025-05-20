using AutoMapper;
using MoneyBankService.Application.Dto;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountDto, BankAccount>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.AccountNumber))
                .ForMember(dest => dest.HolderName, opt => opt.MapFrom(src => src.OwnerName))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.BalanceAmount))
                .ReverseMap()
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.HolderName))
                .ForMember(dest => dest.BalanceAmount, opt => opt.MapFrom(src => src.Balance));
        }
    }
}