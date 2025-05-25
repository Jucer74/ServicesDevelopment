using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccountsAsync(string? accountNumber = null);
    Task<Account?> GetAccountByIdAsync(int id);
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(int id, Account account);
    Task DeleteAccountAsync(int id);
    Task<Account> DepositAsync(int id, decimal amount);
    Task<Account> WithdrawAsync(int id, decimal amount);
}