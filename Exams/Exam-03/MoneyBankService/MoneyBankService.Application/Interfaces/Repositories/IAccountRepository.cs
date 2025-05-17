using MoneyBankService.Domain.Common;
using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Interfaces.Repositories
{

    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetByAccountNumber(string accountNumber);

    }
}