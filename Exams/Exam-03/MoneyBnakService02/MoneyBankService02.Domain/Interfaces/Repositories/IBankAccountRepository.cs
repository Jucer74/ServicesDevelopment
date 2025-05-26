using MoneyBankService02.Domain.Common;
using MoneyBankService02.Domain.Entities;
using System.Linq.Expressions;

namespace MoneyBankService02.Domain.Interfaces.Repositories;

public interface IBankAccountRepository : IRepository<BankAccount>
{
    new Task<BankAccount> GetByIdAsync(int id);
    new Task<IEnumerable<BankAccount>> GetAllAsync();
    new Task<IEnumerable<BankAccount>> FindAsync(Expression<Func<BankAccount, bool>> predicate);
    new Task<BankAccount> AddAsync(BankAccount entity);
    new Task UpdateAsync(BankAccount entity);
    new Task RemoveAsync(BankAccount entity);
}
