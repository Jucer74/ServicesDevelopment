using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BankApp.BL;
using BankApp.DAL;
using BankApp.Entities;

namespace BankApp.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            BankAccountRepository bankAccountRepository = new BankAccountRepository(httpClient);
            BankAccountService bankAccountService = new BankAccountService(bankAccountRepository);

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
                            OverdraftAmount = (AccountType)accountType == AccountType.Checking ? 1000000 : 0
                        };

                        await bankAccountService.CreateAccountAsync(account);
                        Console.WriteLine("Account created successfully!");
                    }
                    else if (option == "2")
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

                        decimal amount;
                        while (true)
                        {
                            Console.Write("Amount to Deposit: ");
                            string? amountInput = Console.ReadLine();

                            if (!decimal.TryParse(amountInput, out amount) || amount <= 0)
                            {
                                Console.WriteLine("Invalid amount. It must be a positive number.");
                            }
                            else
                            {
                                break;
                            }
                        }

                        await bankAccountService.DepositAsync(accountNumber, amount);
                        Console.WriteLine("Deposit successful!");
                    }
                    else if (option == "3")
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

                        decimal amount;
                        while (true)
                        {
                            Console.Write("Amount to Withdraw: ");
                            string? amountInput = Console.ReadLine();

                            if (!decimal.TryParse(amountInput, out amount) || amount <= 0)
                            {
                                Console.WriteLine("Invalid amount. It must be a positive number.");
                            }
                            else
                            {
                                break;
                            }
                        }

                        await bankAccountService.WithdrawAsync(accountNumber, amount);
                        Console.WriteLine("Withdrawal successful!");
                    }
                    else if (option == "4")
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

                        var account = await bankAccountService.GetAccountAsync(accountNumber);
                        if (account != null)
                        {
                            Console.WriteLine($"Balance: {account.BalanceAmount}");
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
