using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces.Services;

public interface IAccountService
{
    Task<Account> CreateAccountAsync(Account account);
    Task<List<Account>> GetAllAccountsAsync();
    Task<List<Account>> GetAccountsByAccountNumberAsync(string accountNumber);
    Task<Account> GetAccountByIdAsync(int accountId);
    Task<Account> UpdateAccount(int accountId, Account newAccount);
    Task DeleteAccountAsync(int accountId);
    Task WithdrawAsync(int accountId, Transaction transaction);
    Task DepositAsync(int accountId, Transaction transaction);
}
