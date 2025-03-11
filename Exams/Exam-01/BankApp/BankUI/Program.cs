using System;
using System.Threading.Tasks;
using Models;
using BankServices;
namespace Models
{
    class Program
    {

        private static BankService _bankService = new BankService();
        static void Main()
        {
            Menu();
        }

        static void Menu()
        {
            while (true)
                {
                Console.WriteLine("\nBank System Menu:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit Amount");
                Console.WriteLine("3. Withdraw Amount");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    DepositAmount();
                    break;
                case "3":
                    WithdrawalAmount();
                    break;
                case "4":
                    GetBalance();
                    break;
                case "0":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
                }
            }
        }
        async static void CreateAccount()
        {
            try {
                
                int accountType = GetValidAccountType();
                string accountNumber = GetValidAccountNumber();
                string accountOwner = GetValidAccountOwnwer();
                decimal initialBalance = GetValidAmount();
                decimal overdraftAmount = (accountType == 1) ? 1000000 : 0;

                BankAccount newAccount = CreateBankAccount(accountType, accountNumber, accountOwner, initialBalance, overdraftAmount);

                await _bankService.CreateAccount(newAccount);
                Console.Write("Account Created Successfully!");
                
            } catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        async static void DepositAmount()
        { 
            try
            {
                string accountNumber = GetValidAccountNumber();
                decimal amount = GetValidAmount();
                var updatedAccount = await _bankService.DepositAmount(accountNumber, amount);
                Console.WriteLine($"Deposit successful! New balance: {updatedAccount.BalanceAmount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async void WithdrawalAmount()
        {
            try {
                string accountNumber = GetValidAccountNumber();
                decimal amount = GetValidAmount();

                await _bankService.WithdrawalAccount(accountNumber, amount);

                Console.WriteLine($"Withdrew {amount} from account {accountNumber}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
        } 

        async static void GetBalance()
        {
            try {
                string accountNumber = GetValidAccountNumber(); 
                BankAccount bankAccount = await _bankService.GetBalance(accountNumber);
                Console.WriteLine($"\nAccount Number: {accountNumber}\n Name: {bankAccount.AccountOwner} \n Balance: {bankAccount.BalanceAmount} \n Overdraft Amount {bankAccount.OverdraftAmount}");
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static bool IsValidAccountNumber(string accountNumber) 
        {
            return accountNumber.Length == 10 && accountNumber.All(char.IsDigit);
        }

        static bool IsValidAmount(string inputAmount)
        {
            return decimal.TryParse(inputAmount, out decimal amount) && amount > 0;

        }

        static bool IsValidAccountOwner(string accountOwner)
        {
            return !string.IsNullOrWhiteSpace(accountOwner) && accountOwner.Length <= 50;
        }

        static int GetValidAccountType()
        {
            int accountType;
            do
            {
                Console.Write("Enter account type (0 for Savings, 1 for Checking): ");
            }
            while (!int.TryParse(Console.ReadLine(), out accountType) || (accountType != 0 && accountType != 1));

            return accountType;
        }

        static string GetValidAccountNumber()
        {
            string accountNumber;
            do
            {   

                Console.WriteLine("Enter account number (10-Digit Number): ");
                accountNumber = Console.ReadLine() ?? string.Empty;
                   
                
            } while (!IsValidAccountNumber(accountNumber));
            return accountNumber;
        }

        static decimal GetValidAmount()
        {
            string input;
            decimal amount;
            
            do
            {
                Console.Write($"Enter Amount : ");
                input = Console.ReadLine() ?? string.Empty;
            } while (!IsValidAmount(input) || !decimal.TryParse(input, out amount));
            return amount;
            
        }

        static string GetValidAccountOwnwer()
        {
            string accountOwner;
            do
            {
                Console.Write("Enter account owner: ");
                accountOwner = Console.ReadLine() ?? string.Empty;
            } while (!IsValidAccountOwner(accountOwner));

            return accountOwner;
        }

        static BankAccount CreateBankAccount(int accountType, string accountNumber, string accountOwner, decimal initialBalance, decimal overdraftAmount)
        {
            var newAccount = new BankAccount(accountNumber, accountOwner, initialBalance, accountType, overdraftAmount);
            return newAccount;
        }
    }
}