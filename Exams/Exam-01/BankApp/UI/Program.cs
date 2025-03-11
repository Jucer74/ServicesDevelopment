using System;
using BankApp.Entitites;
using BankApp.Services;
namespace BankApp
{
    class Program
    {   
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly BankService _bankService = new BankService(_httpClient);

        static void Main(string[] args)
        {   
            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string? input = ReadUserInput();
                int option = ValidateOption(input?? "");

                switch (option)
                {
                    case 1:
                    
                        CreateAccount();
                        break;
                    case 2:
                        GetBalance();
                        break;
                    case 3:
                       DepositAmount();
                        break;
                    case 4:
                        WithdrawalAmount();
                        break;
                    case 5:
                        Exit();
                        return;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\nWelcome to the Bank App");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Get Balance Account");
            Console.WriteLine("3. Deposit Account");
            Console.WriteLine("4. Withdrawal Account");
            Console.WriteLine("5. Exit");
            Console.Write("Select an Option: ");
        }

        private static string? ReadUserInput()
        {
            return Console.ReadLine();
        }

        private static int ValidateOption(string input)
        {
            while (true)
            {
                if (int.TryParse(input, out int option) && option >= 1 && option <= 5)
                    return option;

                Console.WriteLine("Invalid entry. Please enter a number between 1 and 5.");
                Console.Write("Select an Option: ");
                input = Console.ReadLine()?? "";
            }
        }

        private static void Exit()
        {
            Console.WriteLine("Good Bye.");
        }
    

   
        private static void ValidateAccountNumber(string accountNumber)
            {
                if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.Length != 10 || !accountNumber.All(char.IsDigit))
                    throw new ArgumentException("Account number must have 10 digits.");
            }

        private static void ValidateAccountOwner(string accountOwner)
            {
                if (string.IsNullOrWhiteSpace(accountOwner) || accountOwner.Length > 50)
                    throw new ArgumentException("Account Owner is required and max length is 50 characters.");
            }

        private static void ValidateAmount(decimal amount)
            {
                if (amount <= 0)
                    throw new ArgumentException("Amount is required and must be numeric and greater than zero.");
            }

        private static void ValidateAccountType(int accountType)
            {
                if (accountType != 1 && accountType != 2)
                    throw new ArgumentException("Account Type must be 1 (Saving) or 2 (Checking).");
            }

    
        private static int GetValidAccountType()
            {
                while (true)
                {
                    Console.Write("Enter Account Type (1-Saving, 2-Checking): ");
                    string input = Console.ReadLine()?? "";
                   if (int.TryParse(input, out int accountType))
                        {
                            try
                            {
                                ValidateAccountType(accountType);
                                return accountType;
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        Console.WriteLine("Invalid account type. Please enter 1 or 2.");

                }
            }

        private static string GetValidAccountNumber()
            {
                while (true)
                {
                    Console.Write("Enter Account Number (10 digits): ");
                    string accountNumber = Console.ReadLine()?? "";

                    try
                    {
                        ValidateAccountNumber(accountNumber);
                        return accountNumber;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        private static decimal GetValidAmount()
            {
                while (true)
                {
                    Console.Write($"Enter the Amount: ");
                    string input = Console.ReadLine()?? "";
                   if (decimal.TryParse(input, out decimal amount))
                    {
                        try
                        {
                            ValidateAmount(amount);
                            return amount;
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                        Console.WriteLine("Invalid amount. It must be numeric and greater than zero.");


                   
                }
            }

        private static string GetValidAccountOwner()
            {
                while (true)
                {
                    Console.Write("Enter Account Owner (max 50 characters): ");
                    string accountOwner = Console.ReadLine()?? "";

                    try
                    {
                        ValidateAccountOwner(accountOwner);
                        return accountOwner;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        private static void CreateAccount()
        {
            string accountNumber = GetValidAccountNumber();
            string accountOwner = GetValidAccountOwner();
            decimal balanceAmount = GetValidAmount();
            int accountType = GetValidAccountType();
           
            CreateBankAccount(accountNumber, accountOwner, balanceAmount, accountType);
        }
        private static void CreateBankAccount(string accountNumber, string accountOwner, decimal balanceAmount, int accountType)
        {
            decimal overdraftAmount = 0; 

            if (accountType == 2)
            {
                overdraftAmount = 1000000; 
            }


            BankAccount newAccount = new BankAccount(accountNumber, accountOwner, balanceAmount + overdraftAmount, accountType, 0);

            try
            {
                BankAccount account = _bankService.CreateAccountAsync(newAccount).Result;
                
                Console.WriteLine($"Account created successfully. Account Number: {account.AccountNumber}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private static void DepositAmount()
        {
            string accountNumber = GetValidAccountNumber();
            decimal amount = GetValidAmount();

            try
            {
                _bankService.DepositAsync(accountNumber, amount).Wait();
                Console.WriteLine($"Amount deposited successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void GetBalance()
        {
            string accountNumber = GetValidAccountNumber();

            try
            {
                BankAccount? account = _bankService.GetAccountAsync(accountNumber).Result;
                
                if (account == null)
                {
                    Console.WriteLine("Error: The account was not found.");
                }
                else
                {
                   string type = account.AccountType == 2 ? "Checking" : "Saving";

                    Console.WriteLine($"Account Number: {account.AccountNumber}");
                    Console.WriteLine($"Account Owner: {account.AccountOwner}");
                    Console.WriteLine($"Account balance: {account.BalanceAmount}");
                    Console.WriteLine($"Account Type: {type}");

                 
                    if (account.AccountType == 2)
                    {
                        Console.WriteLine($"Overdraft Amount: {account.OverdraftAmount}");
                    }

    
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void WithdrawalAmount()
        {
            string accountNumber = GetValidAccountNumber()?? "";
            decimal amount = GetValidAmount();

            try
            {
                _bankService.WithdrawalAsync(accountNumber, amount).Wait();
                Console.WriteLine($"Amount withdrawn successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}