using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;

// Entities
namespace Entities
{
    public abstract class BankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; private set; }

        public AccountType AccountType { get; private set; }

        public BankAccount(string accountNumber, string accountOwner, decimal balanceAmount, AccountType accountType)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = balanceAmount;
            AccountType = accountType;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount > 0) BalanceAmount += amount;
        }

        public abstract void Withdrawal(decimal amount);
    }

    
    public enum AccountType
    {
        Saving,
        Checking
    }
    

    public class SavingAccount : BankAccount
    {
        public SavingAccount(string accountNumber, string accountOwner, decimal balanceAmount)
            : base(accountNumber, accountOwner, balanceAmount, AccountType.Saving) { }

        public override void Withdrawal(decimal amount)
        {
            if (BalanceAmount >= amount)
            {
                BalanceAmount -= amount;
            }
            else
            {
                Console.WriteLine("Fondos insuficientes.");
            }
        }
    }


    public class CheckingAccount : BankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1000000m;
        public decimal OverdraftAmount { get; private set; }

        public CheckingAccount(string accountNumber, string accountOwner, decimal balanceAmount)
            : base(accountNumber, accountOwner, balanceAmount, AccountType.Checking)
        {
            OverdraftAmount = MIN_OVERDRAFT_AMOUNT;
        }

        public override void Withdrawal(decimal amount)
        {
            if (BalanceAmount - amount >= -OverdraftAmount)
            {
                BalanceAmount -= amount;
            }
            else
            {
                Console.WriteLine("Fondos insuficientes para el sobregiro permitido.");
            }
        }
    }


}

// Services
namespace Services
{
    public class BankService 
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:3000/accounts";

        public BankService()
        {
            _httpClient = new HttpClient();
        }

        public BankAccount CreateAccount(BankAccount bankAccount)
            {
                var json = JsonSerializer.Serialize(bankAccount);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync(_baseUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    var createdAccount = JsonSerializer.Deserialize<BankAccount>(responseJson);
                    return createdAccount;
                }
                return null;
            }

        public BankAccount GetBalanceAccount(string accountNumber)
            {
                var response = _httpClient.GetAsync($"{_baseUrl}?accountNumber={accountNumber}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var accounts = JsonSerializer.Deserialize<List<BankAccount>>(json);
                    return accounts?.Count > 0 ? accounts[0] : null;
                }
                return null;
            }

        public BankAccount DepositAmount(string accountNumber, decimal amountValue)
            {
                var account = GetBalanceAccount(accountNumber);
                if (account != null)
                {
                    account.BalanceAmount += amountValue;
                    return account; 
                }
                return null;
            }

        public BankAccount WithdrawalAmount(string accountNumber, decimal amountValue)
            {
                var account = GetBalanceAccount(accountNumber);
                if (account != null && (account.BalanceAmount - amountValue) >= -account.OverdraftAmount)
                {
                    account.BalanceAmount -= amountValue;
                    return account; 
                }
                return null;
            }

    }
}

// UI
namespace UI
{
    using Services;
    using Entities;
    
   using System;

class Program
{
    static void Main()
    {
        BankService bankService = new BankService();
        Menu(bankService);
    }

    public static void Menu(BankService bankService)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== Bank Management System ===");
            Console.WriteLine("1 - Create Account");
            Console.WriteLine("2 - Get Balance Account");
            Console.WriteLine("3 - Deposit Account");
            Console.WriteLine("4 - Withdrawal Account");
            Console.WriteLine("0 - Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount(bankService);
                    break;
                case "2":
                    GetBalance(bankService);
                    break;
                case "3":
                    DepositAmount(bankService);
                    break;
                case "4":
                    WithdrawAmount(bankService);
                    break;
                case "0":
                    exit = true;
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public static void CreateAccount(BankService bankService)
    {
        Console.Write("Enter account type (1 - Savings, 2 - Checking): ");
        AccountType accountType = (Console.ReadLine() == "1") ? AccountType.Saving : AccountType.Checking;

        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();

        Console.Write("Enter account owner: ");
        string accountOwner = Console.ReadLine();

        Console.Write("Balance Amount: ");
        decimal balance = decimal.Parse(Console.ReadLine());

        
    }

    public static void GetBalance(BankService bankService)
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();

        var account = await bankService.GetBalanceAccount(accountNumber);
        Console.WriteLine($"Account Balance: {account.BalanceAmount}");
    }

    public static void DepositAmount(BankService bankService)
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();

        Console.Write("Enter deposit amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        var account = await bankService.DepositAmount(accountNumber, amount);
        Console.WriteLine($"New Balance: {account.BalanceAmount}");
    }

    public static void WithdrawAmount(BankService bankService)
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();

        Console.Write("Enter withdrawal amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        var account = await bankService.WithdrawalAmount(accountNumber, amount);
        Console.WriteLine($"New Balance: {account.BalanceAmount}");
    }


        
        static bool IsValidAccountNumber(string accountNumber)
        {
            // Implementar validación
            return true;
        }
        
        static bool IsValidAmount(string inputAmount)
        {
            // Implementar validación
            return true;
        }
        
        static bool IsValidAccountOwner(string accountOwner)
        {
            // Implementar validación
            return true;
        }
        
        static AccountType GetValidAccountType()
        {
            // Implementar captura
            return AccountType.Saving;
        }
        
        static string GetValidAccountNumber()
        {
            // Implementar captura
            return "";
        }
        
        static decimal GetValidAmount(string titleAmount)
        {
            // Implementar captura
            return 0;
        }
        
        static string GetValidAccountOwner()
        {
            // Implementar captura
            return "";
        }


       


    }
}
