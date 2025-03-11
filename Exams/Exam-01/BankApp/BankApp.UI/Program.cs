using System;
using System.Threading.Tasks;
using BankApp.Entities;
using BankApp.Services;

namespace BankApp.UI;

class Program
{
    static async Task Main()
    {
        var bankService = new BankService();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n--- Bank Application ---");
            Console.WriteLine("1- Create Account");
            Console.WriteLine("2- Get Balance Account");
            Console.WriteLine("3- Deposit Account");
            Console.WriteLine("4- Withdraw Account");
            Console.WriteLine("0- Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateAccount(bankService);
                    break;
                case "2":
                    GetBalance(bankService);
                    break;
                case "3":
                    Deposit(bankService);
                    break;
                case "4":
                    Withdraw(bankService);
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CreateAccount(BankService bankService)
    {
        Console.Write("Enter Account Number (10 digits): ");
        string accountNumber = Console.ReadLine();
        if (accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
        {
            Console.WriteLine("Invalid account number. It must be exactly 10 digits.");
            return;
        }

        Console.Write("Enter Account Owner Name: ");
        string owner = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(owner) || owner.Length > 50)
        {
            Console.WriteLine("Invalid name. It must be under 50 characters.");
            return;
        }

        Console.Write("Enter Initial Balance: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal balance) || balance < 0)
        {
            Console.WriteLine("Invalid balance amount.");
            return;
        }

        Console.Write("Select Account Type (1 - Saving, 2 - Checking): ");
        string typeInput = Console.ReadLine();
        BankAccount account = typeInput == "1"
            ? new SavingAccount(accountNumber, owner, balance)
            : new CheckingAccount(accountNumber, owner, balance);

        bankService.CreateAccount(account);
        Console.WriteLine("Account created successfully!");
    }

    static void GetBalance(BankService bankService)
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        try
        {
            decimal balance = bankService.GetBalance(accountNumber);
            Console.WriteLine($"Account Balance: {balance:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void Deposit(BankService bankService)
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter Deposit Amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Invalid deposit amount.");
            return;
        }

        try
        {
            bankService.Deposit(accountNumber, amount);
            Console.WriteLine("Deposit successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void Withdraw(BankService bankService)
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter Withdrawal Amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Invalid withdrawal amount.");
            return;
        }

        try
        {
            bankService.Withdraw(accountNumber, amount);
            Console.WriteLine("Withdrawal successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}