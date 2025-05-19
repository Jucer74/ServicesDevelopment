using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(Account account);
        Task<List<Account>> GetAccountByNumber(string accountNumber);
        Task DeleteAccount(int id);
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccountById(int id);
        Task<Account> UpdateAccount(int id, Account account);
        Task UpdateDepositValue(int accountId, Transaction transaction); 
        Task UpdateWithdrawalValue(int accountId, Transaction transaction);
    }
}

