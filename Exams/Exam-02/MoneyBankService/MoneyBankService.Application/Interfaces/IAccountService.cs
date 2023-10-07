using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);

    Task DeleteAccount(int id);

    Task<IEnumerable<Account>> GetAllAccounts();

    Task<Account> GetAccountById(int id);

    Task<Account> UpdateAccount(int id, Account account);

    Task Deposit(int id, Transaction transaction);

    Task Withdrawal(int id, Transaction transaction);

    Task<IEnumerable<Account>> GetAccountsByAccountNumberAsync(string accountNumber);




}