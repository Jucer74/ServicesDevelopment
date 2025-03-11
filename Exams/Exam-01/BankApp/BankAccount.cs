using System;
using System.Text.Json.Serialization;

public enum AccountType
{
    Saving = 1,
    Checking = 2
}

public interface IBankAccount
{
    string AccountNumber { get; set; }
    string AccountOwner { get; set; }
    decimal BalanceAmount { get; set; }
    AccountType AccountType { get; set; }

    void Deposit(decimal amount);
    void Withdrawal(decimal amount);
}

public class SavingAccount : IBankAccount
{   
    public string AccountNumber { get; set; }
    public string AccountOwner { get; set; }
    public decimal BalanceAmount { get; set; }
    public AccountType AccountType { get; set; }

    public SavingAccount(string accountNumber, string accountOwner, decimal initialBalance)
    {
        if (accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
            throw new ArgumentException("El número de cuenta debe tener 10 dígitos.");
        if (accountOwner.Length > 50)
            throw new ArgumentException("El nombre no puede tener más de 50 caracteres.");
        if (initialBalance < 0)
            throw new ArgumentException("El saldo inicial debe ser mayor o igual a cero.");

        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        BalanceAmount = initialBalance;
        AccountType = AccountType.Saving;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("El monto a depositar debe ser mayor a cero.");
        BalanceAmount += amount;
    }

    public void Withdrawal(decimal amount)
    {
        if (amount <= 0 || amount > BalanceAmount)
            throw new ArgumentException("Fondos insuficientes para el retiro.");
        BalanceAmount -= amount;
    }
}

public class CheckingAccount : IBankAccount
{
    private const decimal MIN_OVERDRAFT_AMOUNT = 1000000;

    public string AccountNumber { get; set; }
    public string AccountOwner { get; set; }
    public decimal BalanceAmount { get; set; }
    public AccountType AccountType { get; set; }
    public decimal OverdraftAmount { get; set; }

    public CheckingAccount(string accountNumber, string accountOwner, decimal initialBalance)
    {
        if (accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
            throw new ArgumentException("El número de cuenta debe tener 10 dígitos.");
        if (accountOwner.Length > 50)
            throw new ArgumentException("El nombre del titular no puede tener más de 50 caracteres.");
        if (initialBalance < 0)
            throw new ArgumentException("El saldo inicial debe ser mayor o igual a cero.");

        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        BalanceAmount = initialBalance + MIN_OVERDRAFT_AMOUNT;
        AccountType = AccountType.Checking;
        OverdraftAmount = MIN_OVERDRAFT_AMOUNT;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("El monto a depositar debe ser mayor a cero.");
        BalanceAmount += amount;
    }

    public void Withdrawal(decimal amount)
    {
        if (amount <= 0 || BalanceAmount - amount < -OverdraftAmount)
            throw new ArgumentException("El monto excede el límite de sobregiro permitido.");
        BalanceAmount -= amount;
    }
}
