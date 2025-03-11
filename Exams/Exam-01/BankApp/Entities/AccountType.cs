// Entities/AccountType.cs

namespace Entities
{
public enum AccountType
{
    Saving = 1,
    Checking = 2
}


}

// Entities/IBankAccount.cs
public interface IBankAccount
{
    string AccountNumber { get; set; }
    string AccountOwner { get; set; }
    decimal BalanceAmount { get; set; }
    AccountType AccountType { get; set; }
    decimal OverdraftAmount { get; set; }

    void Deposit(decimal amount);
    void Withdrawal(decimal amount);
}

// Entities/SavingAccount.cs
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

// Entities/CheckingAccount.cs
public class CheckingAccount : IBankAccount
{
    private const decimal MIN_OVERDRAFT_AMOUNT = 1000000;

    public string AccountNumber { get; set; }
    public string AccountOwner { get; set; }
    public decimal BalanceAmount { get; set; }
    public AccountType AccountType { get; set; } = AccountType.Checking;
    public decimal OverdraftAmount { get; set; } = MIN_OVERDRAFT_AMOUNT;

    public void Deposit(decimal amount)
    {
        BalanceAmount += amount;
    }

    public void Withdrawal(decimal amount)
    {
        if (BalanceAmount + OverdraftAmount >= amount)
        {
            BalanceAmount -= amount;
        }
        else
        {
            throw new InvalidOperationException("Insufficient funds");
        }
    }
}