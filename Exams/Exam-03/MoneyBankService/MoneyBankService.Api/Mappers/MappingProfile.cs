using AutoMapper;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
