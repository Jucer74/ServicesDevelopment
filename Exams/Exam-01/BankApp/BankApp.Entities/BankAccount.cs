﻿using System;
using System.Text.Json;

namespace BankApp.Entities
{
    public enum AccountType
    {
        Saving,
        Checking
    }

    public interface IBankAccount
    {
        string AccountNumber { get; set; }
        string AccountOwner { get; set; }
        decimal BalanceAmount { get; set; }
        AccountType AccountType { get; set; }
        void Deposit(decimal amount);
        void Withdrawal(decimal amount);
    }

    public class SavingAccount : IBankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Saving;

        public void Deposit(decimal amount) => BalanceAmount += amount;

        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount >= amount)
                BalanceAmount -= amount;
            else
                throw new InvalidOperationException("Insufficient funds.");
        }
    }

    public class CheckingAccount : IBankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1000000;

        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Checking;
        public decimal OverdraftAmount { get; set; } = MIN_OVERDRAFT_AMOUNT;

        public void Deposit(decimal amount) => BalanceAmount += amount;

        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount + OverdraftAmount >= amount)
                BalanceAmount -= amount;
            else
                throw new InvalidOperationException("Overdraft limit exceeded.");
        }
    }

 public static class BankAccountFactory
{
    public static IBankAccount FromJson(JsonElement element)
    {
        if (!element.TryGetProperty("AccountNumber", out var accountNumberElement))
        {
            throw new InvalidOperationException("Missing 'accountNumber' property in JSON.");
        }

        if (!element.TryGetProperty("AccountOwner", out var accountOwnerElement))
        {
            throw new InvalidOperationException("Missing 'accountOwner' property in JSON.");
        }

        if (!element.TryGetProperty("BalanceAmount", out var balanceAmountElement))
        {
            throw new InvalidOperationException("Missing 'balanceAmount' property in JSON.");
        }

        if (!element.TryGetProperty("AccountType", out var accountTypeElement))
        {
            throw new InvalidOperationException("Missing 'accountType' property in JSON.");
        }

        // Get the AccountType as an integer (since it's represented as a number in JSON)
        var accountType = accountTypeElement.GetInt32(); // Corrected to GetInt32
        var accountNumber = accountNumberElement.GetString();
        var accountOwner = accountOwnerElement.GetString();
        var balanceAmount = balanceAmountElement.GetDecimal();

        if (accountType == 1) // Checking account
        {
            if (!element.TryGetProperty("OverdraftAmount", out var overdraftAmountElement))
            {
                throw new InvalidOperationException("Missing 'overdraftAmount' property for Checking account.");
            }

            return new CheckingAccount
            {
                AccountNumber = accountNumber,
                AccountOwner = accountOwner,
                BalanceAmount = balanceAmount,
                OverdraftAmount = overdraftAmountElement.GetDecimal()
            };
        }
        else if (accountType == 0) // Saving account
        {
            return new SavingAccount
            {
                AccountNumber = accountNumber,
                AccountOwner = accountOwner,
                BalanceAmount = balanceAmount
            };
        }
        else
        {
            throw new InvalidOperationException($"Unknown account type: {accountType}");
        }
    }
}
}
