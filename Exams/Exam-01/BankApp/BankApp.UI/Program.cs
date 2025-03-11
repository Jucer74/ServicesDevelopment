using System;
using System.Net.Http;
using System.Threading.Tasks;
using BankApp.Entities;
using BankApp.BL;

namespace BankApp.UI
{
    class Program
    {
        private static BankService _bankService;

        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var repository = new BankAccountJsonRepository(httpClient);
            _bankService = new BankService(repository);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== Bank App ======");
                Console.WriteLine("1 - Create Account");
                Console.WriteLine("2 - Get Balance");
                Console.WriteLine("3 - Deposit");
                Console.WriteLine("4 - Withdrawal");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("======================");
                Console.Write("Option: ");
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
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static async Task CreateAccount()
        {
            Console.Write("Account Type (1-Saving, 2-Checking): ");
            if (!int.TryParse(Console.ReadLine(), out int accountTypeInt) || (accountTypeInt != 1 && accountTypeInt != 2))
            {
                Console.WriteLine("Invalid account type.");
                Console.ReadKey();
                return;
            }

            AccountType accountType = (AccountType)accountTypeInt;

            Console.Write("Account Number (10 digits): ");
            string accountNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.Length != 10)
            {
                Console.WriteLine("Invalid account number.");
                Console.ReadKey();
                return;
            }

            Console.Write("Account Owner: ");
            string owner = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(owner))
            {
                Console.WriteLine("Invalid owner name.");
                Console.ReadKey();
                return;
            }

            Console.Write("Balance Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal balanceAmount) || balanceAmount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                Console.ReadKey();
                return;
            }

            await _bankService.CreateAccount(accountType, accountNumber, owner, balanceAmount);
            Console.WriteLine("Account Created.");
            Console.ReadKey();
        }

        static async Task GetBalance()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.Length != 10)
            {
                Console.WriteLine("Invalid account number.");
                Console.ReadKey();
                return;
            }

            var account = await _bankService.GetBalance(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
            }
            else
            {
                Console.WriteLine($"Account: {account.AccountNumber} | Owner: {account.AccountOwner} | Balance: {account.BalanceAmount}");
            }
            Console.ReadKey();
        }

        static async Task Deposit()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Amount to deposit: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                Console.ReadKey();
                return;
            }

            await _bankService.DepositAccount(accountNumber, amount);
            Console.WriteLine("Deposit Success.");
            Console.ReadKey();
        }

        static async Task Withdrawal()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Amount to withdraw: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                Console.ReadKey();
                return;
            }

            await _bankService.WithdrawalAccount(accountNumber, amount);
            Console.WriteLine("Withdrawal Success.");
            Console.ReadKey();
        }
    }
}
