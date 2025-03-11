using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Entities;

namespace BankApp.Services;

public class BankService
{
    private readonly List<IBankAccount> _accounts = new();

    public void CreateAccount(IBankAccount account)
    {
        if (_accounts.Any(a => a.AccountNumber == account.AccountNumber))
            throw new InvalidOperationException("Account already exists.");
        
        _accounts.Add(account);
    }

    public IBankAccount GetAccount(string accountNumber)
    {
        var account = _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
            throw new InvalidOperationException("Account not found.");
        return account;
    }

    public decimal GetBalance(string accountNumber)
    {
        var account = GetAccount(accountNumber);
        return account.BalanceAmount;
    }

    public void Deposit(string accountNumber, decimal amount)
    {
        var account = GetAccount(accountNumber);
        account.Deposit(amount);
    }

    public void Withdraw(string accountNumber, decimal amount)
    {
        var account = GetAccount(accountNumber);
        account.Withdrawal(amount);
    }
}
