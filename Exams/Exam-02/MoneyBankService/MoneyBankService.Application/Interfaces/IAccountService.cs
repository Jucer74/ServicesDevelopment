using MoneyBankService.Domain.Entities;
using System.Transactions;


namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
    Task DeleteAccount(int id);
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task<Account> UpdateAccount(int id, Account account);


    Task<Account> DepositToAccount(int id, Domain.Entities.Transaction transaction);

    Task<Account> WithDrawalToAccount(int id, Domain.Entities.Transaction transaction);
    Task<Account> WithdrawalFromAccount(int id, Domain.Entities.Transaction transaction);
}