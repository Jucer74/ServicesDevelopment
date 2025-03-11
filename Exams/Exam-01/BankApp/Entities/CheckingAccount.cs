namespace Entities
{
    public class CheckingAccount : BankAccount
    {
        public CheckingAccount(string accountNumber, string accountOwner)
            : base(accountNumber, accountOwner)
        {
            AccountType = AccountType.Checking;
            OverdraftAmount = 1000000;
            BalanceAmount += OverdraftAmount;
        }

        public override void Withdrawal(decimal amount)
        {
            if (BalanceAmount + OverdraftAmount >= amount)
            {
                BalanceAmount -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds including overdraft");
            }
        }
    }

}