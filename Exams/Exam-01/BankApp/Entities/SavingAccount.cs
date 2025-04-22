namespace Entities
{
    public class SavingAccount : BankAccount
    {
        public SavingAccount(string accountNumber, string accountOwner)
            : base(accountNumber, accountOwner)
        {
            AccountType = AccountType.Saving;
        }
    }
}