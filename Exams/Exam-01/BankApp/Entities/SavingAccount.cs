namespace BankApp.Entities
{
    public class SavingAccount : BankAccount
    {
        public SavingAccount(string accountNumber, string accountOwner, decimal balanceAmount)
            : base(accountNumber, accountOwner, balanceAmount, AccountType.Saving) { }

        public override bool Withdrawal(decimal amount)
        {
            if (amount > BalanceAmount) return false;
            BalanceAmount -= amount;
            return true;
        }
    }
}
