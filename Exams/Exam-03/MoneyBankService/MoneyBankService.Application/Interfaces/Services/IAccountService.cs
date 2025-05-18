using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Interfaces.Services;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);

    Task<List<Account>> GetAllAccounts();

    Task<Account> GetAccountById(int id);

    Task<Account> UpdateAccount(int id, Account account);

    Task DeleteAccount(int id);

    Task<Account?> GetAccountByNumberAccount(string accountNumber);
}
