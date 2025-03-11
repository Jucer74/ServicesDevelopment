using BankApp.Interfaces;

namespace BankApp.Entities
{
    public class SavingAccount : IBankAccount
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        public SavingAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }
}
