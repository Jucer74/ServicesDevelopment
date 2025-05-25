using MoneyBankService.Domain.Common;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Domain.Interfaces.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetByAccountNumberAsync(string accountNumber);
}