using AutoMapper;
using MoneyBankService.Application.Dto; // Namespace actualizado para DTOs
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo bidireccional entre Account y AccountDto
            CreateMap<Account, AccountDto>().ReverseMap();

          
            /*
            CreateMap<TransactionDto, Account>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                // .ForMember(dest => dest.BalanceAmount, opt => opt.Ignore()) 
                
                ;
            */
        }
    }
}