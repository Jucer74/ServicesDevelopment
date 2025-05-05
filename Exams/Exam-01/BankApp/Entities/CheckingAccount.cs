namespace BankApp.Entities
{
    public class CheckingAccount : BankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1000000m;
        public decimal OverdraftAmount { get; private set; }

        public CheckingAccount(string accountNumber, string accountOwner, decimal balanceAmount)
            : base(accountNumber, accountOwner, balanceAmount + MIN_OVERDRAFT_AMOUNT, AccountType.Checking)
        {
            OverdraftAmount = 0;
        }

        public override bool Withdrawal(decimal amount)
        {
            if (amount > BalanceAmount + OverdraftAmount) return false;
            BalanceAmount -= amount;
            if (BalanceAmount < 0)
            {
                OverdraftAmount = -BalanceAmount;
                BalanceAmount = 0;
            }
            return true;
        }
    }
}
