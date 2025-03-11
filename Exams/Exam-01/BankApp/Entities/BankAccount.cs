using System;
using System.Text.Json.Serialization;

namespace Entities
{
    public class BankAccount
    {
        // Propiedades
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountType AccountType { get; set; }
        public decimal OverdraftAmount { get; set; }

        public BankAccount(string accountNumber, string accountOwner)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
        }

        public virtual void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }

        public virtual void Withdrawal(decimal amount)
        {
            if (BalanceAmount >= amount)
            {
                BalanceAmount -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            }
        }
    }
}