using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using BankApp.DAL;
using BankApp.Entities;

namespace BankApp.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            BankAccountRepository accountService = new BankAccountRepository();

            while (true)
            {
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Check Balance");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string? option = Console.ReadLine()?.Trim();

                try
                {
                    if (option == "1") 
                    {
                        string? accountNumber;
                        while (true)
                        {
                            Console.Write("Account Number: ");
                            accountNumber = Console.ReadLine();

                            if (string.IsNullOrEmpty(accountNumber) || !Regex.IsMatch(accountNumber, @"^\d{10}$"))
                            {
                                Console.WriteLine("Invalid account number. It must be exactly 10 digits.");
                            }
                            else
                            {
                                break;
                            }
                        }

                        string? accountOwner;
                        while (true)
                        {
                            Console.Write("Account Owner: ");
                            accountOwner = Console.ReadLine();

                            if (string.IsNullOrEmpty(accountOwner) || !Regex.IsMatch(accountOwner, @"^[a-zA-Z\s]+$") || accountOwner.Length > 50)
                            {
                                Console.WriteLine("Invalid account owner. It must contain only letters and be no more than 50 characters.");
                            }
                            else
                            {
                                break;
                            }
                        }

                        int accountType;
                        while (true)
                        {
                            Console.Write("Account Type (1-Saving, 2-Checking): ");
                            string? accountTypeInput = Console.ReadLine();

                            if (!int.TryParse(accountTypeInput, out accountType) || (accountType != 1 && accountType != 2))
                            {
                                Console.WriteLine("Invalid account type. It must be 1 (Saving) or 2 (Checking).");
                            }
                            else
                            {
                                break;
                            }
                        }

                        decimal balanceAmount;
                        while (true)
                        {
                            Console.Write("Initial Balance: ");
                            string? balanceInput = Console.ReadLine();

                            if (!decimal.TryParse(balanceInput, out balanceAmount) || balanceAmount < 0)
                            {
                                Console.WriteLine("Invalid balance. It must be a positive number.");
                            }
                            else
                            {
                                break;
                            }
                        }

                        var account = new BankAccount
                        {
                            AccountNumber = accountNumber,
                            AccountOwner = accountOwner,
                            BalanceAmount = balanceAmount,
                            AccountType = (AccountType)accountType,
                            OverdraftAmount = accountType == 2 ? 1000000 : 0,
                        };

                        await accountService.AddAccountAsync(account);
                        Console.WriteLine("Account created successfully!");
                    }
                    else if (option == "2") 
                    {
                        Console.Write("Account Number: ");
                        string? accountNumber = Console.ReadLine();

                        if (string.IsNullOrEmpty(accountNumber))
                        {
                            Console.WriteLine("Account number cannot be empty.");
                            continue;
                        }

                        Console.Write("Amount to Deposit: ");
                        string? amountInput = Console.ReadLine();
                        if (!decimal.TryParse(amountInput, out decimal amount) || amount <= 0)
                        {
                            Console.WriteLine("Invalid amount. It must be a positive number.");
                            continue;
                        }

                        await accountService.DepositAsync(accountNumber, amount);
                        Console.WriteLine("Deposit successful!");
                    }
                    else if (option == "3") 
                    {
                        Console.Write("Account Number: ");
                        string? accountNumber = Console.ReadLine();

                        if (string.IsNullOrEmpty(accountNumber))
                        {
                            Console.WriteLine("Account number cannot be empty.");
                            continue;
                        }

                        Console.Write("Amount to Withdraw: ");
                        string? amountInput = Console.ReadLine();
                        if (!decimal.TryParse(amountInput, out decimal amount) || amount <= 0)
                        {
                            Console.WriteLine("Invalid amount. It must be a positive number.");
                            continue;
                        }

                        await accountService.WithdrawAsync(accountNumber, amount);
                        Console.WriteLine("Withdrawal successful!");
                    }
                    else if (option == "4") 
                    {
                        Console.Write("Account Number: ");
                        string? accountNumber = Console.ReadLine();

                        if (string.IsNullOrEmpty(accountNumber))
                        {
                            Console.WriteLine("Account number cannot be empty.");
                            continue;
                        }

                        var account = await accountService.GetAccountAsync(accountNumber);
                        if (account != null)
                        {
                            Console.WriteLine($"Balance: {account.BalanceAmount}");
                            Console.WriteLine($"Account Type: {account.AccountType}");
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                    }
                    else if (option == "0") 
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
