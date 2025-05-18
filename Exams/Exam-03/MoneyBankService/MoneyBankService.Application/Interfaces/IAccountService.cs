using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    Task Deposit(int id, Transaction transaction);
    Task Withdrawal(int id, Transaction transaction);
}