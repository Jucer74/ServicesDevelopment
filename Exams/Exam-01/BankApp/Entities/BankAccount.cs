using System;
using System.Text.Json.Serialization;

namespace BankApp.Entities
{
    public class BankAccount : IBankAccount
    {
        public string id {get; set;}
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public AccountType AccountType { get; set; }

        public decimal OverdraftAmount { get; set; } 

        public BankAccount() { }

        public BankAccount(string AccountNumber, string AccountOwner, decimal BalanceAmount, AccountType accountType)

        {
            id = AccountNumber;
            AccountNumber = AccountNumber;
            AccountNumber = AccountOwner;
            BalanceAmount = BalanceAmount;
            AccountType = AccountType;
            OverdraftAmount = (AccountType == AccountType.Checking) ? 1000000m : 0m;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");
            BalanceAmount += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0) return false;

            if (AccountType == AccountType.Saving)
            {
                if (BalanceAmount >= amount)
                {
                    BalanceAmount -= amount;
                    return true;
                }
            }
            else if (AccountType == AccountType.Checking)
            {
                if (BalanceAmount - amount >= -OverdraftAmount)
                {
                    BalanceAmount -= amount;
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
