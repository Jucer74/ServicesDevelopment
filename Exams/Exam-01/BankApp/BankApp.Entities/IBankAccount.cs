namespace BankApp.Entities
{
    public enum AccountType
    {
        Saving = 1,
        Checking = 2
    }

    public interface IBankAccount
    {
        string AccountNumber { get; }
        string AccountOwner { get; }
        decimal BalanceAmount { get; }
        AccountType AccountType { get; }

        void Deposit(decimal amount);
        void Withdrawal(decimal amount);
    }
}
