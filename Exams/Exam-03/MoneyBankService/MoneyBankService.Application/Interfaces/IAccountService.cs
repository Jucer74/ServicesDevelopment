using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
     public  async Task<List<Account>> GetAllAccounts()
    {
        return (await IAccountRepository.GetAllAsync()).ToList();
    }

    Task<Account> GetAccountById(int id);

    Task<Account> UpdateAccount(int id, Account account);
    Task DeleteAccount(int id);

    Task<List<Account>> GetAccounts(string? accountNumber = null);

    Task Deposit(int id, Transaction transaction);
    Task Withdraw(int id, Transaction transaction);

}