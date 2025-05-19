using MoneyBankService.Domain.Common;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(Guid id);
        Task<Account> CreateAsync(Account account);
        Task<bool> UpdateAsync(Account account);
        Task<bool> DeleteAsync(Guid id);
    }
}