using System.Transactions;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService { 

    Task<IEnumerable<Account>> GetAllAccounts();

    Task<Account> GetAccountById(int id);

    Task<Account> UpdateAcount(int id, Account account);

    Task<Account> CreateAcount(Account account);

    Task DeleteAccount(int id);

    Task<Account> DepositToAccount(int id, Domain.Entities.Transaction transaction);

    Task<Account> WithDrawalToAccount(int id, Domain.Entities.Transaction transaction);
}