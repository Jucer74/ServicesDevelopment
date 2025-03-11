using System;
using System.Linq;

namespace BankApp.Entities
{
    public class SavingAccount : IBankAccount
    {
        public string AccountNumber { get; private set; }
        public string AccountOwner { get; private set; }
        public decimal BalanceAmount { get; private set; }
        public AccountType AccountType => AccountType.Saving;

        public SavingAccount(string accountNumber, string accountOwner, decimal initialBalance)
        {
            if (accountNumber.Length != 10 || !accountNumber.All(char.IsDigit))
                throw new ArgumentException("Account number must have 10 digits.");

            if (string.IsNullOrWhiteSpace(accountOwner) || accountOwner.Length > 50)
                throw new ArgumentException("Account owner is required and max length is 50 characters.");

            if (initialBalance <= 0)
                throw new ArgumentException("Initial balance must be greater than zero.");

            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");
            BalanceAmount += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            if (BalanceAmount < amount)
                throw new InvalidOperationException("Insufficient funds.");
            BalanceAmount -= amount;
        }
    }
}
