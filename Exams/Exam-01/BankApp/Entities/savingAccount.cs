namespace Entities
{
    public class SavingAccount : IBankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Saving;
        public decimal OverdraftAmount { get; set; } = 0;

        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }

        public void Withdrawal(decimal amount)
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