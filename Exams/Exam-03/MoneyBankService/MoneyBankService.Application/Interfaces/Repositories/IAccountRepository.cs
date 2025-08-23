using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<List<Account>> GetByNumberAccountAsync(string accountNumber);
    }
}
