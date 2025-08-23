using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Interfaces.Services;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);

    Task<List<Account>> GetAllAccounts();
    Task<List<Account>> GetAccountByNumber(string accountNumber);

    Task<Account> GetAccountById(int id);

    Task<Account> UpdateAccount(int id, Account account);

    Task DeleteAccount(int id);

    Task Deposit(int id, Transaction transaction);
    Task Withdrawal(int id, Transaction transaction);

}
