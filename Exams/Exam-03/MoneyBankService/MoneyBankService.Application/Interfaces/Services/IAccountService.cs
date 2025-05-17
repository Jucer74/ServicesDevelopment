

using MoneyBankService.Application.Dto;
using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Interfaces.Services;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
    Task<Account> GetAccountById(int id);
    Task<List<Account>> GetAllAccounts();
    Task<Account> UpdateAccount(int id, Account account);
    Task DeleteAccount(int id);
    Task<Account?> GetAccountByAccountNumber(string accountNumber);
    Task Deposit(int id, Transaction transaction);
    Task Withdraw(int id, Transaction transaction);
    
}
