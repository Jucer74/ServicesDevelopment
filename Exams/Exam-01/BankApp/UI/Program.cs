using BankApp.Services;
using BankApp.Entities;
using System;
using System.Threading.Tasks;

namespace BankApp.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BankService bankService = new BankService();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n1- Create Account");
                Console.WriteLine("2- Get Balance Account");
                Console.WriteLine("3- Deposit Account");
                Console.WriteLine("4- Withdrawal Account");
                Console.WriteLine("0- Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        await CreateAccount(bankService);
                        break;
                    case "2":
                        await GetBalance(bankService);
                        break;
                    case "3":
                        await Deposit(bankService);
                        break;
                    case "4":
                        await Withdrawal(bankService);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static async Task CreateAccount(BankService bankService)
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Account Owner: ");
            string accountOwner = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Account Type (1 = Saving, 2 = Checking): ");
            int type = Convert.ToInt32(Console.ReadLine());

            decimal overdraftAmount = 0;
            if (type == 2) // Si es cuenta corriente, preguntar por el sobregiro
            {
                Console.Write("Enter Overdraft Limit: ");
                overdraftAmount = Convert.ToDecimal(Console.ReadLine());
            }

            BankAccount account = new BankAccount
            {
                AccountNumber = accountNumber,
                AccountOwner = accountOwner,
                BalanceAmount = balance,
                AccountType = type == 1 ? AccountType.Saving : AccountType.Checking,
                OverdraftAmount = overdraftAmount
            };

            bool success = await bankService.CreateAccountAsync(account);
            if (success) Console.WriteLine("Account created successfully!");
        }

        static async Task GetBalance(BankService bankService)
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();
            var account = await bankService.GetBalanceAsync(accountNumber);

            if (account != null)
                Console.WriteLine($"Balance: {account.BalanceAmount}");
        }

        static async Task Deposit(BankService bankService)
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            await bankService.DepositAsync(accountNumber, amount);
        }

        static async Task Withdrawal(BankService bankService)
        {
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            await bankService.WithdrawalAsync(accountNumber, amount);
        }
    }
}
