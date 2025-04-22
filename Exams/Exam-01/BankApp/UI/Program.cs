using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Entities;
using Microsoft.Extensions.Configuration;

namespace UI
{
    class Program
    {
         public async Task Run(BankService bankService)
        {
            while (true)
            {
                Console.Clear();
                Menu();
                var choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        await CreateAccount(bankService);
                        break;
                    case "2":
                        await GetBalance(bankService);
                        break;
                    case "3":
                        await DepositAmount(bankService);
                        break;
                    case "4":
                        await WithdrawalAmount(bankService);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void Menu()
        {
            Console.WriteLine("Banking Operations");
            Console.WriteLine("------------------");
            Console.WriteLine("1- Create Account\n2- Get Balance\n3- Deposit Amount\n4- Withdrawal Amount\n0- Exit");
            Console.Write("Select Option: ");
        }

        static async Task CreateAccount(BankService bankService)
        {
            Console.Clear();
            Console.WriteLine("Create Account");
            Console.WriteLine("--------------");
            var accountType = GetValidAccountType();
            var accountNumber = GetValidAccountNumber();
            var accountOwner = GetValidAccountOwner();
            var initialBalanceAmount = GetValidAmount("Balance Amount");

            BankAccount account = accountType == AccountType.Saving
                ? new SavingAccount(accountNumber, accountOwner)
                : new CheckingAccount(accountNumber, accountOwner, initialBalanceAmount);


            try
            {
                await bankService.CreateAccountAsync(account);
                Console.WriteLine("Account Created");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static async Task GetBalance(BankService bankService)
        {
            Console.WriteLine("Get Balance");
            Console.WriteLine("-----------");
            var accountNumber = GetValidAccountNumber();
            var account = await bankService.GetBalanceAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine("La cuenta no existe.");
                return;
            }

            Console.WriteLine($"Account Type= {account.AccountType}");
            Console.WriteLine($"Placeholder= {account.AccountOwner}");
            Console.WriteLine($"Balance Amount= {account.BalanceAmount}");

            if (account is CheckingAccount checkingAccount)
            {
                decimal overdraftAvailable = checkingAccount.OverdraftLimit - checkingAccount.OverdraftUsed;
                Console.WriteLine($"Overdraft Amount: {checkingAccount.OverdraftUsed}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static async Task DepositAmount(BankService bankService)
        {
            Console.WriteLine("Deposit Amount");
            Console.WriteLine("--------------");
            var accountNumber = GetValidAccountNumber();
            var amount = GetValidAmount("Amount to Deposit");
        
            try
            {
                var account = await bankService.DepositAsync(accountNumber, amount);
                Console.WriteLine("Deposit Success");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static async Task WithdrawalAmount(BankService bankService)
        {
            Console.WriteLine("Withdrawal Amount");
            Console.WriteLine("-----------------");
            var accountNumber = GetValidAccountNumber();
            var amount = GetValidAmount("Amount to Withdraw");
            try
            {
                var account = await bankService.WithdrawalAsync(accountNumber, amount);
                Console.WriteLine("Withdrawal Success");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static AccountType GetValidAccountType()
        {
            while (true)
            {
                Console.Write("Account Type (1-Saving, 2-Checking): ");
                var input = Console.ReadLine();
                if (input == "1") return AccountType.Saving;
                if (input == "2") return AccountType.Checking;
                Console.WriteLine("Invalid account type. Please enter 1 for Saving or 2 for Checking.");
            }
        }

        static string GetValidAccountNumber()
        {
            while (true)
            {
                Console.Write("Account Number: ");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Account number cannot be empty. Please try again.");
                    continue;
                }

                if (input.Length == 10 && input.All(char.IsDigit))
                {
                    return input;
                }

                Console.WriteLine("Account number must be exactly 10 digits.");
            }
        }

        static string GetValidAccountOwner()
        {
            while (true)
            {
                Console.Write("Account Owner: ");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.Length <= 50) return input;
                Console.WriteLine("Account owner is required and must be 50 characters or less.");
            }
        }

        static decimal GetValidAmount(string titleAmount)
        {
            while (true)
            {
                Console.Write($"{titleAmount}: ");
                var input = Console.ReadLine();
                if (decimal.TryParse(input, out var amount) && amount > 0) return amount;
                Console.WriteLine("Amount must be a numeric value greater than zero.");
            }
        }
    }
}