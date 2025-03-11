namespace Entities
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; }
        public decimal OverdraftAmount { get; set; }

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