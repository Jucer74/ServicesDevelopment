using System;

namespace BankApp
{
    public enum AccountType
    {
        Saving = 1,   // Ahorros
        Checking = 2  // Corriente
    }

    public class BankAccount
    {   
        public int id { get; set; }
        public string AccountNumber { get; set; }   
        public string AccountOwner { get; set; }   
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } 
        public decimal OverdraftAmount { get; set; }


        public BankAccount(string accountNumber, string accountOwner, decimal balanceAmount, AccountType accountType, decimal overdraftAmount = 0)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = balanceAmount;
            AccountType = accountType;
            OverdraftAmount = overdraftAmount;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El monto a depositar debe ser mayor a cero.");
            BalanceAmount += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El monto a retirar debe ser mayor a cero.");
            
            if (BalanceAmount + OverdraftAmount < amount)
                throw new InvalidOperationException("Fondos insuficientes para la transacción.");

            BalanceAmount -= amount;
        }
    }
}
