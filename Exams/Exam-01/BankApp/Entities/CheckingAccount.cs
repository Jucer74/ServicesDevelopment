namespace Entities
{
    public class CheckingAccount : BankAccount
    {
        public decimal OverdraftLimit { get; set; } = 1000000;
        public decimal OverdraftUsed { get; set; } = 0;

        public CheckingAccount(string accountNumber, string accountOwner, decimal initialBalance = 0)
            : base(accountNumber, accountOwner)
        {
            AccountType = AccountType.Checking;
            BalanceAmount = initialBalance;
        }

        public override void Withdrawal(decimal amount)
        {
            if (BalanceAmount >= amount)
            {
                BalanceAmount -= amount;
            }
            else if (BalanceAmount + (OverdraftLimit - OverdraftUsed) >= amount)
            {
                decimal overdraftNeeded = amount - BalanceAmount;
                BalanceAmount = 0;
                OverdraftUsed += overdraftNeeded;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds including overdraft");
            }
        }
    }
}