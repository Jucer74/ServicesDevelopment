using BankApp.Interfaces;
using BankApp.Entities;
using BankApp.Services;
using System;
using BankApp.Enums;


namespace BankApp
{
    	
    class Program
{
    static void Main(string[] args)
    {
        BankService bankService = new BankService();

        while (true)
        {
            Menu();
            string option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        CreateAccount(bankService);
                        break;
                    case "2":
                        GetBalance(bankService);
                        break;
                    case "3":
                        DepositAmount(bankService);
                        break;
                    case "4":
                        WithdrawalAmount(bankService);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void Menu()
    {
        Console.WriteLine("1- Create Account");
        Console.WriteLine("2- Get Balance Account");
        Console.WriteLine("3- Deposit Amount");
        Console.WriteLine("4- Withdrawal Amount");
        Console.WriteLine("0- Exit");
        Console.Write("Select an option: ");
    }

    static void CreateAccount(BankService bankService)
    {
        Console.WriteLine("Create Account");
        Console.WriteLine("--------------");

        AccountType accountType = GetValidAccountType();
        string accountNumber = GetValidAccountNumber();
        string accountOwner = GetValidAccountOwner();
        decimal initialBalanceAmount = GetValidAmount("Initial Balance Amount");

        IBankAccount account = CreateBankAccount(accountType, accountNumber, accountOwner, initialBalanceAmount);
        bankService.CreateAccount(account);
        Console.WriteLine("Account Created");
    }

    static void GetBalance(BankService bankService)
    {
        Console.WriteLine("Get Balance");
        Console.WriteLine("-----------");

        string accountNumber = GetValidAccountNumber();
        var account = bankService.GetBalanceAccount(accountNumber);

        Console.WriteLine($"Account Type: {account.AccountType}");
        Console.WriteLine($"Account Owner: {account.AccountOwner}");
        Console.WriteLine($"Balance Amount: {account.BalanceAmount}");
        if (account is CheckingAccount checkingAccount)
        {
            Console.WriteLine($"Overdraft Amount: {checkingAccount.OverdraftAmount}");
        }
    }

    static void DepositAmount(BankService bankService)
    {
        Console.WriteLine("Deposit Amount");
        Console.WriteLine("--------------");

        string accountNumber = GetValidAccountNumber();
        decimal amount = GetValidAmount("Amount to Deposit");

        var account = bankService.DepositAmount(accountNumber, amount);
        Console.WriteLine("Deposit Success");
    }

    static void WithdrawalAmount(BankService bankService)
    {
        Console.WriteLine("Withdrawal Amount");
        Console.WriteLine("-----------------");

        string accountNumber = GetValidAccountNumber();
        decimal amount = GetValidAmount("Amount to Withdraw");

        var account = bankService.WithdrawalAmount(accountNumber, amount);
        Console.WriteLine("Withdrawal Success");
    }

    static bool IsValidAccountNumber(string accountNumber)
    {
        return accountNumber.Length == 10 && accountNumber.All(char.IsDigit);
    }

    static bool IsValidAmount(string inputAmount)
    {
        return decimal.TryParse(inputAmount, out decimal amount) && amount > 0;
    }

    static bool IsValidAccountOwner(string accountOwner)
    {
        return !string.IsNullOrWhiteSpace(accountOwner) && accountOwner.Length <= 50;
    }

    static AccountType GetValidAccountType()
    {
        while (true)
        {
            Console.Write("AccountType (1-Saving, 2-Checking): ");
            if (Enum.TryParse(Console.ReadLine(), out AccountType accountType) && Enum.IsDefined(typeof(AccountType), accountType))
            {
                return accountType;
            }
            Console.WriteLine("Invalid account type. Please enter 1 for Saving or 2 for Checking.");
        }
    }

    static string GetValidAccountNumber()
    {
        while (true)
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            if (IsValidAccountNumber(accountNumber))
            {
                return accountNumber;
            }
            Console.WriteLine("Account number must be exactly 10 digits.");
        }
    }

    static decimal GetValidAmount(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string inputAmount = Console.ReadLine();
            if (IsValidAmount(inputAmount))
            {
                return decimal.Parse(inputAmount);
            }
            Console.WriteLine("Amount must be a positive number.");
        }
    }

    static string GetValidAccountOwner()
    {
        while (true)
        {
            Console.Write("Account Owner: ");
            string accountOwner = Console.ReadLine();
            if (IsValidAccountOwner(accountOwner))
            {
                return accountOwner;
            }
            Console.WriteLine("Account owner is required and must be 50 characters or less.");
        }
    }

    static IBankAccount CreateBankAccount(AccountType accountType, string accountNumber, string accountOwner, decimal initialBalanceAmount)
    {
        return accountType == AccountType.Saving ?
            new SavingAccount { AccountNumber = accountNumber, AccountOwner = accountOwner, BalanceAmount = initialBalanceAmount } :
            new CheckingAccount { AccountNumber = accountNumber, AccountOwner = accountOwner, BalanceAmount = initialBalanceAmount };
    }
    }        

}
