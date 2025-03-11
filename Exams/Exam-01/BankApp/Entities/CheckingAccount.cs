using BankApp.Interfaces;

namespace BankApp.Entities
{
    public class CheckingAccount : IBankAccount
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public decimal OverdraftAmount { get; private set; }

        public CheckingAccount(string accountNumber, decimal initialBalance, decimal overdraftAmount)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            OverdraftAmount = overdraftAmount;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (Balance + OverdraftAmount >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }
}
