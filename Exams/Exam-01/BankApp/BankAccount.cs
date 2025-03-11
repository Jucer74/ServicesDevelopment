using System.Text.Json;
using Newtonsoft.Json;

namespace BankApp
{

    public class BankAccount
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty("account_owner")]
        public string AccountOwner { get; set; }
        [JsonProperty("balance_amount")]
        public decimal BalanceAmount { get; set; }
        [JsonProperty("account_type")]
        public AccountType AccountType { get; set; }

        [JsonProperty("overdraft_amount")]
        public decimal OverdraftAmount { get; set; }

        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The amount to be withdrawn must be greater than 0.");
            }

            if (AccountType == AccountType.Checking) // Checking = 2
            {
                decimal totalFunds = BalanceAmount + OverdraftAmount;

                if (amount > totalFunds)
                {
                    throw new InvalidOperationException("Insufficient funds, even considering the overdraft.");
                }

                if (amount > BalanceAmount)
                {
                    decimal overdraftUsed = amount - BalanceAmount;
                    OverdraftAmount -= overdraftUsed;
                    BalanceAmount = 0;
                }
                else
                {
                    BalanceAmount -= amount;
                }
            }
            else // Saving Account
            {
                if (amount > BalanceAmount)
                {
                    throw new InvalidOperationException("Insufficient funds for the operation.");
                }

                BalanceAmount -= amount;
            }
        }
    }

    public enum AccountType
    {
        Saving = 1,
        Checking = 2
    }
}