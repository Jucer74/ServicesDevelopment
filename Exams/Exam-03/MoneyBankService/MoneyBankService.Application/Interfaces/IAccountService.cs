using MoneyBankService.Application.Dto;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
    Task<List<Account>> GetAllAccounts();

    Task<Account> GetAccountById(int id);

    Task<Account> UpdateAccount(int id, Account account);
    Task DeleteAccount(int id);

    Task<List<Account>> GetAccounts(string? accountNumber = null);
    Task<Account> DepositAsync(int accountId, decimal amount);
    Task<Account> WithdrawAsync(int accountId, decimal amount);


}
