namespace BankApp.Entities
{
    public interface IBankAccount
    {
        string AccountNumber { get; }
        string Owner { get; }
        decimal Balance { get; }

        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void ShowBalance();
    }
}


