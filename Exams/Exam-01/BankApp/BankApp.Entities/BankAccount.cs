namespace BankApp.Entities
{
    public enum AccountType
    {
        Saving,
        Checking
    }

    public interface IBankAccount
    {
        string AccountNumber { get; set; }
        string AccountOwner { get; set; }
        decimal BalanceAmount { get; set; }
        AccountType AccountType { get; set; }
        void Deposit(decimal amount);
        void Withdrawal(decimal amount);
    }

    public class SavingAccount : IBankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Saving;

        public void Deposit(decimal amount) => BalanceAmount += amount;

        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount >= amount)
                BalanceAmount -= amount;
            else
                throw new InvalidOperationException("Insufficient funds.");
        }
    }

    public class CheckingAccount : IBankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 100000;

        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Checking;
        public decimal OverdraftAmount { get; set; } = MIN_OVERDRAFT_AMOUNT;

        public void Deposit(decimal amount) => BalanceAmount += amount;

        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount + OverdraftAmount >= amount)
                BalanceAmount -= amount;
            else
                throw new InvalidOperationException("Overdraft limit exceeded.");
        }
    }
}
