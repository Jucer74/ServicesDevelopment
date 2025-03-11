using System;
using System.Linq;

namespace BankApp.Entities
{
    public class CheckingAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1_000_000m;

        public decimal OverdraftAmount { get; private set; }
        public AccountType AccountType { get; private set; }
         public string AccountNumber { get; set; } // Ahora es editable
        public string AccountOwner { get; set; }  // Ahora es editable
        public decimal BalanceAmount { get; set; }

        public CheckingAccount() { }

        public CheckingAccount(string accountNumber, string accountOwner, decimal initialBalance)
        {
            if (accountNumber.Length != 10 || !accountNumber.All(char.IsDigit))
                throw new ArgumentException("Account number must have 10 digits.");

            if (string.IsNullOrWhiteSpace(accountOwner) || accountOwner.Length > 50)
                throw new ArgumentException("Account owner is required and max length is 50 characters.");

            if (initialBalance <= 0)
                throw new ArgumentException("Initial balance must be greater than zero.");

            AccountNumber = accountNumber;
            AccountOwner = accountOwner;

            // El saldo inicia sumando el valor mínimo de sobregiro
            BalanceAmount = initialBalance + MIN_OVERDRAFT_AMOUNT;
            OverdraftAmount = 0;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            BalanceAmount += amount;

            
            if (OverdraftAmount > 0)
            {
                var cubrir = Math.Min(OverdraftAmount, BalanceAmount);
                OverdraftAmount -= cubrir;
                BalanceAmount += 0; // Ajustamos el saldo después de cubrir el sobregiro
            }
        }

        public void Withdrawal(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");


            decimal availableFunds = BalanceAmount + MIN_OVERDRAFT_AMOUNT - OverdraftAmount;
            if (amount > availableFunds)
                throw new InvalidOperationException("Insufficient funds.");

            BalanceAmount -= amount;


            if (BalanceAmount < 0)
            {
                OverdraftAmount = -BalanceAmount;
            }
        }
    }
}
