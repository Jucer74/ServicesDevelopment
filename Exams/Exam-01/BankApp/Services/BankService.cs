// Services/BankService.cs
using BankApp.Interfaces;
using BankApp.Entities;
using System.Collections.Generic;


namespace Services
{

    public class BankService
{
    private readonly List<IBankAccount> _accounts = new List<IBankAccount>();

    public IBankAccount CreateAccount(IBankAccount bankAccount)
    {
        if (_accounts.Any(a => a.AccountNumber == bankAccount.AccountNumber))
        {
            throw new InvalidOperationException("Account already exists");
        }

        _accounts.Add(bankAccount);
        return bankAccount;
    }

    public IBankAccount GetBalanceAccount(string accountNumber)
    {
        var account = _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            throw new InvalidOperationException("Account does not exist");
        }

        return account;
    }

    public IBankAccount DepositAmount(string accountNumber, decimal amountValue)
    {
        var account = GetBalanceAccount(accountNumber);
        account.Deposit(amountValue);
        return account;
    }

    public IBankAccount WithdrawalAmount(string accountNumber, decimal amountValue)
    {
        var account = GetBalanceAccount(accountNumber);
        account.Withdrawal(amountValue);
        return account;
    }
}

}

