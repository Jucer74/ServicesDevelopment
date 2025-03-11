namespace Entities
{
    public class SavingAccount : BankAccount
    {
        public SavingAccount()
        {
            AccountType = AccountType.Saving;
        }
    }
}