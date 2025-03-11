using System;
using System.Threading.Tasks;
using BankApp.BL;
using BankApp.Entities;

namespace BankApp.UI
{
    class Program
    {
        private static readonly BankService _bankService = new BankService();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1- Create Account");
                Console.WriteLine("2- Get Balance");
                Console.WriteLine("3- Deposit");
                Console.WriteLine("4- Withdrawal");
                Console.WriteLine("0- Exit");
                Console.Write("Choose an option: ");
                
                string? option = Console.ReadLine();
                if (option == null)
                {
                    Console.WriteLine("Invalid input, try again.");
                    continue;
                }

                switch (option)
                {
                    case "1":
                        await CreateAccount();
                        break;
                    case "2":
                        await GetBalance();
                        break;
                    case "3":
                        await Deposit();
                        break;
                    case "4":
                        await Withdrawal();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        private static async Task CreateAccount()
        {
            string accountNumber = GetValidAccountNumber();
            string accountOwner = GetValidAccountOwner();
            decimal balanceAmount = GetValidDecimalInput("Enter Initial Balance: ");
            int type = GetValidAccountType();

            IBankAccount account = type == 1
                ? new SavingAccount { AccountNumber = accountNumber, AccountOwner = accountOwner, BalanceAmount = balanceAmount }
                : new CheckingAccount { AccountNumber = accountNumber, AccountOwner = accountOwner, BalanceAmount = balanceAmount };

            await _bankService.CreateAccountAsync(account);
            Console.WriteLine("Account created successfully!");
        }

        private static async Task GetBalance()
        {
            Console.Write("Enter Account Number: ");
            string? accountNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Invalid account number.");
                return;
            }

            try
            {
                decimal balance = await _bankService.GetBalanceAsync(accountNumber);
                Console.WriteLine($"Balance: {balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static async Task Deposit()
        {
            string accountNumber = GetValidAccountNumber();
            decimal amount = GetValidDecimalInput("Enter Amount: ");
            await _bankService.DepositAsync(accountNumber, amount);
            Console.WriteLine("Deposit successful.");
        }

        private static async Task Withdrawal()
        {
            string accountNumber = GetValidAccountNumber();
            decimal amount = GetValidDecimalInput("Enter Amount: ");
            await _bankService.WithdrawalAsync(accountNumber, amount);
            Console.WriteLine("Withdrawal successful.");
        }

        // Helper method to get a valid account number
        private static string GetValidAccountNumber()
        {
            string? accountNumber;
            while (true)
            {
                Console.Write("Enter Account Number (10 digits): ");
                accountNumber = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(accountNumber) && accountNumber.Length == 10 && long.TryParse(accountNumber, out _))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid account number. Please enter a valid 10-digit number.");
                }
            }
            return accountNumber;
        }

        // Helper method to get a valid account owner name
        private static string GetValidAccountOwner()
        {
            string? accountOwner;
            while (true)
            {
                Console.Write("Enter Account Owner Name: ");
                accountOwner = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(accountOwner))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Account owner name cannot be empty. Please enter a valid name.");
                }
            }
            return accountOwner;
        }

        // Helper method to get a valid decimal input
        private static decimal GetValidDecimalInput(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && decimal.TryParse(input, out value) && value >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a positive number.");
                }
            }
            return value;
        }

        // Helper method to get a valid account type (1 for Saving, 2 for Checking)
        private static int GetValidAccountType()
        {
            int type;
            while (true)
            {
                Console.Write("Select Account Type (1: Saving, 2: Checking): ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out type) && (type == 1 || type == 2))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid account type. Please enter 1 for Saving or 2 for Checking.");
                }
            }
            return type;
        }
    }
}
