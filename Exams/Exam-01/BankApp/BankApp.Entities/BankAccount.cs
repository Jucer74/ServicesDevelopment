namespace BankApp.Entities;

public enum AccountType
{
    Saving,
    Checking
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

public class SavingAccount : IBankAccount
{
    public string AccountNumber { get; private set; }
    public string AccountOwner { get; private set; }
    public decimal BalanceAmount { get; private set; }
    public AccountType AccountType { get; private set; }

    public SavingAccount(string accountNumber, string accountOwner, decimal balanceAmount)
    {
        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        BalanceAmount = balanceAmount;
        AccountType = AccountType.Saving;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be greater than zero.");
        BalanceAmount += amount;
    }

    public void Withdrawal(decimal amount)
    {
        if (amount > BalanceAmount)
            throw new InvalidOperationException("Insufficient funds.");
        BalanceAmount -= amount;
    }
}

public class CheckingAccount : IBankAccount
{
    private const decimal MIN_OVERDRAFT_AMOUNT = 1000000m;
    
    public string AccountNumber { get; private set; }
    public string AccountOwner { get; private set; }
    public decimal BalanceAmount { get; private set; }
    public AccountType AccountType { get; private set; }
    public decimal OverdraftAmount { get; private set; }

    public CheckingAccount(string accountNumber, string accountOwner, decimal balanceAmount)
    {
        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        BalanceAmount = balanceAmount + MIN_OVERDRAFT_AMOUNT;
        AccountType = AccountType.Checking;
        OverdraftAmount = MIN_OVERDRAFT_AMOUNT;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be greater than zero.");
        BalanceAmount += amount;
    }

    public void Withdrawal(decimal amount)
    {
        if (BalanceAmount - amount < -OverdraftAmount)
            throw new InvalidOperationException("Withdrawal exceeds overdraft limit.");
        BalanceAmount -= amount;
    }
}
