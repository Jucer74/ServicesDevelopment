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
                
                string option = Console.ReadLine();
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
            Console.Write("Enter Account Number (10 digits): ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Account Owner Name: ");
            string accountOwner = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            decimal balanceAmount = decimal.Parse(Console.ReadLine());

            Console.Write("Select Account Type (1: Saving, 2: Checking): ");
            int type = int.Parse(Console.ReadLine());


            IBankAccount account = type == 1
                ? new SavingAccount { AccountNumber = accountNumber, AccountOwner = accountOwner, BalanceAmount = balanceAmount }
                : new CheckingAccount { AccountNumber = accountNumber, AccountOwner = accountOwner, BalanceAmount = balanceAmount };

            await _bankService.CreateAccountAsync(account);
            Console.WriteLine("Account created successfully!");
        }

        private static async Task GetBalance()
{
    Console.Write("Enter Account Number: ");
    string accountNumber = Console.ReadLine();

    try
    {
        decimal balance = await _bankService.GetBalanceAsync(accountNumber);
        Console.WriteLine($"Balance: {balance}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


        private static async Task Deposit()
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            await _bankService.DepositAsync(accountNumber, amount);
            Console.WriteLine("Deposit successful.");
        }

        private static async Task Withdrawal()
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            await _bankService.WithdrawalAsync(accountNumber, amount);
            Console.WriteLine("Withdrawal successful.");
        }
    }
}
