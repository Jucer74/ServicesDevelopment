using System;
using System.Linq;
using System.Net.Http;
using BankApp.Entities;
using BankApp.Services;

namespace BankApp.UI
{
    class Program
    {
        private static IBankService _bankService;

        static void Main(string[] args)
        {
            // =========== REPOSITORIO EN MEMORIA ===========
            // var repo = new BankAccountRepository();
            // _bankService = new BankService(repo);

            // =========== REPOSITORIO CON JSON-SERVER ===========
            var httpClient = new HttpClient();
            var jsonRepo = new BankAccountJsonRepository(httpClient);
            _bankService = new BankService(jsonRepo);

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
                        CreateAccount();
                        break;
                    case "2":
                        GetBalance();
                        break;
                    case "3":
                        Deposit();
                        break;
                    case "4":
                        Withdrawal();
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

        static void CreateAccount()
        {
            try
            {
                Console.Write("Account Type (1-Saving, 2-Checking): ");
                string typeInput = Console.ReadLine();
                if (!int.TryParse(typeInput, out int accountTypeInt) 
                    || (accountTypeInt != 1 && accountTypeInt != 2))
                {
                    Console.WriteLine("Invalid account type. Must be 1 or 2.");
                    Console.ReadKey();
                    return;
                }
                AccountType accountType = (AccountType)accountTypeInt;

                // Número de cuenta
                Console.Write("Account Number (10 digits): ");
                string accountNumber = Console.ReadLine();
                if (!IsValidAccountNumber(accountNumber))
                {
                    Console.WriteLine("Account number must have exactly 10 digits.");
                    Console.ReadKey();
                    return;
                }

                // Propietario
                Console.Write("Account Owner: ");
                string owner = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(owner) || owner.Length > 50)
                {
                    Console.WriteLine("Owner is required and max length is 50 characters.");
                    Console.ReadKey();
                    return;
                }

                // Monto inicial
                Console.Write("Balance Amount: ");
                string balanceInput = Console.ReadLine();
                if (!decimal.TryParse(balanceInput, out decimal balanceAmount) || balanceAmount <= 0)
                {
                    Console.WriteLine("Amount is required and must be numeric and greater than zero.");
                    Console.ReadKey();
                    return;
                }

                if (_bankService.ExistsAccount(accountNumber))
                {
                    Console.WriteLine($"Account {accountNumber} already exists.");
                    Console.ReadKey();
                    return;
                }

                _bankService.CreateAccount(accountType, accountNumber, owner, balanceAmount);
                Console.WriteLine("Account Created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void GetBalance()
        {
            try
            {
                Console.Write("Account Number: ");
                string accountNumber = Console.ReadLine();
                if (!IsValidAccountNumber(accountNumber))
                {
                    Console.WriteLine("Account number must have exactly 10 digits.");
                    Console.ReadKey();
                    return;
                }

                var account = _bankService.GetBalance(accountNumber);
                Console.WriteLine("--------- Balance Info ---------");
                Console.WriteLine($"Account Number:  {account.AccountNumber}");
                Console.WriteLine($"Owner:           {account.AccountOwner}");
                Console.WriteLine($"Type:            {account.AccountType}");
                Console.WriteLine($"Balance Amount:  {account.BalanceAmount}");

                if (account is CheckingAccount checking)
                {
                    Console.WriteLine($"Overdraft Amount: {checking.OverdraftAmount}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void Deposit()
        {
            try
            {
                Console.Write("Account Number: ");
                string accountNumber = Console.ReadLine();
                if (!IsValidAccountNumber(accountNumber))
                {
                    Console.WriteLine("Account number must have exactly 10 digits.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Amount to deposit: ");
                string amountInput = Console.ReadLine();
                if (!decimal.TryParse(amountInput, out decimal amount) || amount <= 0)
                {
                    Console.WriteLine("Amount is required and must be numeric and greater than zero.");
                    Console.ReadKey();
                    return;
                }

                _bankService.DepositAccount(accountNumber, amount);
                Console.WriteLine("Deposit Success.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void Withdrawal()
        {
            try
            {
                Console.Write("Account Number: ");
                string accountNumber = Console.ReadLine();
                if (!IsValidAccountNumber(accountNumber))
                {
                    Console.WriteLine("Account number must have exactly 10 digits.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Amount to withdraw: ");
                string amountInput = Console.ReadLine();
                if (!decimal.TryParse(amountInput, out decimal amount) || amount <= 0)
                {
                    Console.WriteLine("Amount is required and must be numeric and greater than zero.");
                    Console.ReadKey();
                    return;
                }

                _bankService.WithdrawalAccount(accountNumber, amount);
                Console.WriteLine("Withdrawal Success.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static bool IsValidAccountNumber(string accountNumber)
        {
            return !string.IsNullOrWhiteSpace(accountNumber) 
                   && accountNumber.Length == 10
                   && accountNumber.All(char.IsDigit);
        }
    }
}
