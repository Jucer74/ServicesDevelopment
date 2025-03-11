using System;

namespace BankApp.Entities
{
    public abstract class BankAccount
    {
        public string AccountNumber { get; }
        public string AccountOwner { get; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; }

        protected BankAccount(string accountNumber, string accountOwner, decimal balanceAmount, AccountType accountType)
        {
            if (accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
            throw new ArgumentException("Account number must have 10 digits.");
            if (string.IsNullOrWhiteSpace(accountOwner) || accountOwner.Length > 50)
                throw new ArgumentException("Account owner is required and max length is 50 characters.");
            if (balanceAmount <= 0)
                throw new ArgumentException("Balance amount must be greater than zero.");

            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = balanceAmount;
            AccountType = accountType;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than zero.");
            BalanceAmount += amount;
        }

        public abstract bool Withdrawal(decimal amount);
    }
}
