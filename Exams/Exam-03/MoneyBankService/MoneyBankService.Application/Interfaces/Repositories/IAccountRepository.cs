using System.Linq.Expressions;
using Application.Common;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<List<Account>> GetByAccountNumberAsync(string accountNumber);
    }
}

