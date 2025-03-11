using System;
using System.Text.Json.Serialization;

namespace BankApp.Entities
{
    public class BankAccount : IBankAccount
    {
        public string AccountNumber { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public AccountType Type { get; set; }

        public decimal OverdraftLimit { get; set; } // Solo para cuentas Corrientes

        public BankAccount() { }

        public BankAccount(string accountNumber, string owner, decimal balance, AccountType type)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = balance;
            Type = type;
            OverdraftLimit = (type == AccountType.Checking) ? 1000000m : 0m;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0) return false;

            if (Type == AccountType.Saving)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    return true;
                }
            }
            else if (Type == AccountType.Checking)
            {
                if (Balance - amount >= -OverdraftLimit)
                {
                    Balance -= amount;
                    return true;
                }
            }
            return false;
        }
    }

    public enum AccountType
    {
        Saving,
        Checking
    }
}
