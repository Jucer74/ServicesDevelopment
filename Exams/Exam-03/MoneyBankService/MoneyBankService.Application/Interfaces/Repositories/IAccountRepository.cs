using MoneyBankService.Application.Common;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Domain.Interfaces.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetByAccountNumber(string accountNumber);
}