using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task<Account> CreateAsync(Account account);
        Task<bool> UpdateAsync(int id, Account account);
        Task<bool> DeleteAsync(int id);
    }
}