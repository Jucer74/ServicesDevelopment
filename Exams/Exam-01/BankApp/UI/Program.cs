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
                        AccountType accountType;

                        if (typeOption == 1)
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
                            Console.WriteLine("Revisa que el numero de cuenta tenga 10 dígitos.");
                            break;
                        }

                        Console.WriteLine("Account Owner: ");
                        string? accountOwner = Console.ReadLine();
                        if (!string.IsNullOrEmpty(accountOwner) && !IsValidAccountOwner(accountOwner))
                        {
                            Console.WriteLine("Revisa que el nombre de propietario.");
                            break;
                        }

                        Console.WriteLine("Balance Amount: ");
                        decimal initialAmount;
                        decimal.TryParse(Console.ReadLine(), out initialAmount);
                        if (!IsValidAmount(initialAmount))
                        {
                            Console.WriteLine("El monto debe ser mayor a 0.");
                            break;
                        }
                        
                        BankAccount bankAccount = CreateBankAccount(accountType, accountNumber, accountOwner, initialAmount);
                        BankAccount result = await bankService.CreateAccount(bankAccount);

                        if (result != null){
                            Console.WriteLine("Cuenta creada con éxito.");
                        } else {
                            Console.WriteLine("La cuenta ya existe o hubo un error al crearla.");
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
                            Console.WriteLine("Revisa que el numero de cuenta tenga 10 dígitos.");
                            break;
                        }

                        bankAccount = await bankService.GetBalance(accountNumber);

                        if (bankAccount == null){
                            Console.WriteLine("Cuenta NO existe.");
                            break;
                        }

                        if (bankAccount.AccountType == AccountType.Saving)
                        {
                            Console.WriteLine("Account Owner: " + bankAccount.AccountOwner);
                            Console.WriteLine("Account Type: " + bankAccount.AccountType);
                            Console.WriteLine("Balance Amount: " + bankAccount.BalanceAmount);
                        } else {
                            Console.WriteLine("Account Owner: " + bankAccount.AccountOwner);
                            Console.WriteLine("Account Type: " + bankAccount.AccountType);
                            Console.WriteLine("Balance Amount: " + bankAccount.BalanceAmount);
                            Console.WriteLine("Overdraft Amount: " + bankAccount.OverdraftAmount);
                        }

                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine(@"
\tDeposit Amount
-----------------------------------
Account Number: ");
                        accountNumber = Console.ReadLine();

                        Console.WriteLine("Amount: ");
                        decimal amount;
                        decimal.TryParse(Console.ReadLine(), out amount);

                        if (!string.IsNullOrEmpty(accountNumber) && !IsValidAccountNumber(accountNumber))
                        {
                            Console.WriteLine("Revisa que el numero de cuenta tenga 10 dígitos.");
                            break;
                        }

                        if (!IsValidAmount(amount))
                        {
                            Console.WriteLine("El monto debe ser mayor a 0.");
                            break;
                        }

                        await bankService.DepositAmount(accountNumber, amount);

                        Console.WriteLine("Deposito realizado.");
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine(@"
\tWithdrawal Amount
-----------------------------------
Account Number: ");
                        accountNumber = Console.ReadLine();

                        Console.WriteLine("Amount: ");
                        decimal.TryParse(Console.ReadLine(), out amount);

                        if (!string.IsNullOrEmpty(accountNumber) && !IsValidAccountNumber(accountNumber))
                        {
                            Console.WriteLine("Revisa que el numero de cuenta tenga 10 dígitos.");
                            break;
                        }

                        if (!IsValidAmount(amount))
                        {
                            Console.WriteLine("El monto debe ser mayor a 0.");
                            break;
                        }

                        await bankService.WithdrawalAmount(accountNumber, amount);

                        Console.WriteLine("Retiro realizado.");
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
            }

            Console.WriteLine("\nPresiona Enter para continuar...");
            Console.ReadLine();
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