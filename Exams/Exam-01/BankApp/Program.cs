using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Entities;
using Services;

class Program
{
    private static BankService _bankService = new BankService();
    
    static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit Amount");
            Console.WriteLine("3. Get Balance");
            Console.WriteLine("4. Withdraw Amount");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateAccount();
                    break;
                case "2":
                    await DepositAmount();
                    break;
                case "3":
                    await GetBalance();
                    break;
                case "4":
                    await WithdrawAmount();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private static async Task CreateAccount()
    {
        string accountNumber;
        string owner;
        decimal balance;
        int accountType;

        while (true)
        {
            Console.Write("Enter Account Number (10 digits): ");
            accountNumber = Console.ReadLine();

            if (!Regex.IsMatch(accountNumber, "^\\d{10}$"))
            {
                Console.WriteLine("Invalid account number. It must be exactly 10 numeric digits.");
                continue;
            }

            var existingAccount = await _bankService.GetBalanceAccount(accountNumber);
            if (existingAccount != null)
            {
                Console.WriteLine("Account number already exists. Choose a different one.");
                continue;
            }

            break;
        }

        Console.WriteLine("Account number is valid and unique.");
        
        while (true)
        {
            Console.Write("Enter Account Owner (max 50 characters): ");
            owner = Console.ReadLine();
            
            if (owner.Length > 50)
            {
                Console.WriteLine("Invalid name. It must be at most 50 characters.");
                continue;
            }
            
            break;
        }
        
        Console.Write("Enter Initial Balance: ");
        while (!decimal.TryParse(Console.ReadLine(), out balance))
        {
            Console.Write("Invalid amount. Enter a valid number: ");
        }
        
        Console.Write("Enter Account Type (0: Checking, 1: Saving): ");
        while (!int.TryParse(Console.ReadLine(), out accountType) || (accountType != 0 && accountType != 1))
        {
            Console.Write("Invalid type. Enter 0 for Checking or 1 for Saving: ");
        }
        
        decimal overdraft = accountType == 0 ? 1000000 : 0;
        
        var account = new BankAccount
        {
            AccountNumber = accountNumber,
            AccountOwner = owner,
            BalanceAmount = balance,
            AccountType = accountType,
            OverdraftAmount = overdraft
        };
        
        await _bankService.CreateAccount(account);
        Console.WriteLine("Account created successfully!");
    }

    private static async Task DepositAmount()
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        
        Console.Write("Enter Deposit Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        
        var account = await _bankService.DepositAmount(accountNumber, amount);
        if (account != null)
            Console.WriteLine($"Deposit successful! New Balance: {account.BalanceAmount}");
        else
            Console.WriteLine("Deposit failed. Account not found.");
    }

    private static async Task GetBalance()
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        
        var account = await _bankService.GetBalanceAccount(accountNumber);
        if (account != null)
            Console.WriteLine($"Account Balance: {account.BalanceAmount}");
        else
            Console.WriteLine("Account not found.");
    }

    private static async Task WithdrawAmount()
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        
        Console.Write("Enter Withdrawal Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        
        var account = await _bankService.WithdrawalAmount(accountNumber, amount);
        if (account != null)
            Console.WriteLine($"Withdrawal successful! New Balance: {account.BalanceAmount}");
        else
            Console.WriteLine("Withdrawal failed. Check balance or account details.");
    }
}