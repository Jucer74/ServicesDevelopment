using MoneyBankService.Application.Common;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Application.Dto;


namespace MoneyBankService.Application.Interfaces.Repositories;

public interface IAccountRepository : IRepository<Account>
{
}