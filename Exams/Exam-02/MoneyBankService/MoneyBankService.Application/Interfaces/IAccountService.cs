using MoneyBankService.Domain.Dto;
using MoneyBankService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<Account> CreateAccount(Account account);
    Task DeleteAccount(int id);
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task<Account> UpdateAccount(int id, Account account);
    Task Deposit(int id, TransactionDto transactionDto);
    Task Withdraw(int id, TransactionDto transactionDto);

    Task<IEnumerable<Account>> FindAccountsByAccountNumber(string accountNumber);
}
