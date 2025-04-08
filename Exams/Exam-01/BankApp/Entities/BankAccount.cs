using System;

namespace BankApp.Entities
{
    public abstract class BankAccount : IBankAccount
    {
        public string AccountNumber { get; }
        public string Owner { get; }
        public decimal Balance { get; protected set; }

        protected BankAccount(string accountNumber, string owner, decimal initialBalance)
        {
            
            if (accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
                throw new ArgumentException("Account number must be exactly 10 numeric digits.");

            
            if (string.IsNullOrWhiteSpace(owner) || owner.Length > 50)
                throw new ArgumentException("Owner name must be valid and no more than 50 characters.");

            
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative.");

            AccountNumber = accountNumber;
            Owner = owner;
            Balance = initialBalance;
        }

        public virtual void Deposit(decimal amount)
        {
            
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than 0.");

            Balance += amount;
            Console.WriteLine($"Deposit successful. New balance: {Balance:C}");
        }

        public abstract void Withdraw(decimal amount);

        public void ShowBalance()
        {
            Console.WriteLine($"Current balance for {Owner} ({AccountNumber}) is: {Balance:C}");
        }
    }
}