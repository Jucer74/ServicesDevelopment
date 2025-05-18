using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces.Services;

public interface IAccountService
{
    Task<List<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task<Account> CreateAccount(Account account);
    Task<Account> UpdateAccount(int id, Account account);
    Task DeleteAccount(int id);
    Task<Account?> GetAccountByAccountNumber(string accountNumber);
    Task Deposit(int id, Transaction transaction);
    Task Withdraw(int id, Transaction transaction);

}
