namespace BankApp
{

    public class BankAccount
    {
        public int AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; }

        public decimal OverdraftAmount { get; set; }

        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }

        public void Withdrawal(decimal amount)
        {
            BalanceAmount -= amount;
        }
    }

    public enum AccountType
    {
        Saving = 1,
        Checking = 2
    }
}