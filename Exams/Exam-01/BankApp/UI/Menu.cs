using System;
using BankApp.Services;
using BankApp.Entities;

namespace BankApp.UI
{
    public static class Menu
    {
        public static void Show()
        {
            var bankService = new BankService();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1- Create Account\n2- Get Balance\n3- Deposit\n4- Withdraw\n0- Exit");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                if (!int.TryParse(option, out int optionNumber) || optionNumber < 0 || optionNumber > 4)
                {
                    Console.WriteLine("\nInvalid option. Please choose a valid option (0-4).");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                if (option == "0") break;

                Console.Write("Account Number (10 digits): ");
                string accountNumber;
                while (true)
                {
                    accountNumber = Console.ReadLine();
                    if (!string.IsNullOrEmpty(accountNumber) && accountNumber.Length == 10 && long.TryParse(accountNumber, out _))
                        break;
                    Console.WriteLine(" Invalid Account Number. It must be exactly 10 digits.");
                    Console.Write("Enter a valid Account Number: ");
                }

                switch (option)
                {
                    case "1":
                        Console.Write("Account Type (1-Saving, 2-Checking): ");
                        var accountType = (AccountType)int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Account Owner: ");
                        string accountOwner = Console.ReadLine();
                        Console.Write("Balance Amount: ");
                        decimal balance = decimal.Parse(Console.ReadLine() ?? "0");
                        try
                        {
                            var account = bankService.CreateAccount(accountNumber, accountOwner, balance, accountType);
                            Console.WriteLine($" Account Created: {account.AccountNumber}");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($" {ex.Message}");
                        }
                        break;
                    case "2":
                        try
                        {
                            var balanceAccount = bankService.GetBalance(accountNumber);
                            Console.WriteLine($"Balance: {balanceAccount.BalanceAmount}");
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine($" {ex.Message}");
                        }
                        break;
                    case "3":
                        Console.Write("Deposit Amount: ");
                        try
                        {
                            bankService.DepositAccount(accountNumber, decimal.Parse(Console.ReadLine() ?? "0"));
                            Console.WriteLine(" Deposit Success");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($" {ex.Message}");
                        }
                        break;
                    case "4":
                        Console.Write("Withdrawal Amount: ");
                        try
                        {
                            bankService.WithdrawalAccount(accountNumber, decimal.Parse(Console.ReadLine() ?? "0"));
                            Console.WriteLine(" Withdrawal Success");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($" {ex.Message}");
                        }
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
