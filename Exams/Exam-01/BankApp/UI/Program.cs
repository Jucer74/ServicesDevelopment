using System;
using System.Text.RegularExpressions;
using BankApp.Services;
using BankApp.Entities.Enum;
using BankApp.Entities.Models;
using System.IO.Pipelines;


public class Program
{

    static async Task Main()
    {
        var configService = new ConfigurationService();
        Console.WriteLine($"Conexión a BD: {configService.GetConnectionString()}");
        Console.WriteLine($"URL de API: {configService.GetApiUrl()}");
        BankService bankService = new BankService();
        await Menu(bankService);
    }

    public static async Task Menu(BankService bankService)
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine(@"
Banking Operation
----------------------
1. Create Account.
2. Get Balance.
3. Deposit Amount.
4. Withdrawal Amount.
0. Exit.
Select Option: ");

            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine(@"
Create Account
--------------
Select account type (1-Saving | 2-Checking): ");
                        int typeOption;
                        int.TryParse(Console.ReadLine(), out typeOption);
                        AccountType accountType = AccountType.Saving;

                        if (typeOption != 1 && typeOption != 2)
                        {
                            Console.WriteLine("Choose an option between 1 or 2");
                            break;

                        }
                        else if (typeOption == 1)
                        {
                            accountType = AccountType.Saving;
                        }
                        else
                        {
                            accountType = AccountType.Checking;
                        }

                        Console.WriteLine("Account Number: ");
                        string? accountNumber = Console.ReadLine();
                        if (!string.IsNullOrEmpty(accountNumber) && !IsValidAccountNumber(accountNumber))
                        {
                            Console.WriteLine("Account number must have 10 digits");
                            break;
                        }

                        Console.WriteLine("Account Owner: ");
                        string? accountOwner = Console.ReadLine();
                        if (!string.IsNullOrEmpty(accountOwner) && !IsValidAccountOwner(accountOwner))
                        {
                            Console.WriteLine("Account owwner name must have only letters.");
                            break;
                        }

                        Console.WriteLine("Balance Amount: ");
                        decimal initialAmount;
                        decimal.TryParse(Console.ReadLine(), out initialAmount);
                        if (!IsValidAmount(initialAmount))
                        {
                            Console.WriteLine("Amount must be greater than 0");
                            break;
                        }

                        BankAccount bankAccount = CreateBankAccount(accountType, accountNumber, accountOwner, initialAmount);

                        try {
                        BankAccount result = await bankService.CreateAccount(bankAccount);
                        Console.WriteLine("Account created successfully");
                        }catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message); // Imprime el mensaje de error en la UI
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine(@"
\tBalance Account
-----------------------------------
Account Number: ");
                        accountNumber = Console.ReadLine();

                        if (!string.IsNullOrEmpty(accountNumber) && !IsValidAccountNumber(accountNumber))
                        {
                            Console.WriteLine("Account number must have 10 digits");
                            break;
                        }

                        try
                        {
                            bankAccount = await bankService.GetBalance(accountNumber);
                            if (bankAccount.AccountType == AccountType.Saving)
                            {
                                Console.WriteLine("Account Owner: " + bankAccount.AccountOwner);
                                Console.WriteLine("Account Type: " + bankAccount.AccountType);
                                Console.WriteLine("Balance Amount: " + bankAccount.BalanceAmount);
                            }
                            else
                            {
                                Console.WriteLine("Account Owner: " + bankAccount.AccountOwner);
                                Console.WriteLine("Account Type: " + bankAccount.AccountType);
                                Console.WriteLine("Balance Amount: " + bankAccount.BalanceAmount);
                                Console.WriteLine("Overdraft Amount: " + bankAccount.OverdraftAmount);
                            }
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message); // Imprime el mensaje de error en la UI
                        }



                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine(@"
\tDeposit Amount
-----------------------------------
Account Number: ");
                        accountNumber = Console.ReadLine();
                        if (!string.IsNullOrEmpty(accountNumber) && !IsValidAccountNumber(accountNumber))
                        {
                            Console.WriteLine("Account number must have 10 digits");
                            break;
                        }

                        Console.WriteLine("Amount: ");
                        decimal amount;
                        decimal.TryParse(Console.ReadLine(), out amount);
                        if (!IsValidAmount(amount))
                        {
                            Console.WriteLine("Amount must be greater than 0");
                            break;
                        }

                        try
                        {
                            bankAccount = await bankService.DepositAmount(accountNumber, amount);
                            Console.WriteLine("Deposit has been made");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message); // Imprime el mensaje de error en la UI
                        }

                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine(@"
\tWithdrawal Amount
-----------------------------------
Account Number: ");
                        accountNumber = Console.ReadLine();
                        if (!string.IsNullOrEmpty(accountNumber) && !IsValidAccountNumber(accountNumber))
                        {
                            Console.WriteLine("Account number must have 10 digits");
                            break;
                        }

                        Console.WriteLine("Amount: ");
                        decimal.TryParse(Console.ReadLine(), out amount);
                        if (!IsValidAmount(amount))
                        {
                            Console.WriteLine("Amount must be greater than 0");
                            break;
                        }

                        try
                        {
                            bankAccount = await bankService.WithdrawalAmount(accountNumber, amount);
                            Console.WriteLine("Successful withdrawal");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message); // Aquí se imprime el error en la UI
                        }
                        break;
                    case 0:
                        Console.WriteLine("Leaving the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        } while (option != 0);

    }

    static bool IsValidAccountNumber(string accountNumber)
    {
        bool digits = Regex.IsMatch(accountNumber, @"^\d{10}$");

        return digits;
    }

    static bool IsValidAccountOwner(string accountOwner)
    {
        bool name = Regex.IsMatch(accountOwner, @"^[a-zA-Z\s]{1,50}$");
        return name;
    }

    static bool IsValidAmount(decimal? amount)
    {
        return amount.HasValue && amount > 0;
    }

    static BankAccount CreateBankAccount(AccountType accountType, string accountNumber, string accountOwner, decimal balanceAmount)
    {
        BankAccount bankAccount = new BankAccount(accountNumber, accountOwner, balanceAmount, accountType);
        return bankAccount;
    }
}