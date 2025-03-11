using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    [JsonConverter(typeof(BankAccountConverter))]
    public abstract class BankAccount
    {
        public string id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; }

        public abstract void Deposit(decimal amount);
        public abstract void Withdrawal(decimal amount);
    }

    public class SavingAccount : BankAccount
    {
        public override void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }
            BalanceAmount += amount;
        }

        public override void Withdrawal(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            }

            if (BalanceAmount >= amount)
            {
                BalanceAmount -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds.");
            }
        }
    }


    public class CheckingAccount : BankAccount
    {
        public decimal OverdraftAmount { get; set; } = 1000000;

        public override void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");
            BalanceAmount += amount;
        }

        public override void Withdrawal(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            if (BalanceAmount + OverdraftAmount >= amount)
                BalanceAmount -= amount;
            else
                throw new InvalidOperationException("Insufficient funds, including overdraft limit.");
        }
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Saving,
        Checking
    }

    public class BankAccountConverter : JsonConverter<BankAccount>
    {
        public override BankAccount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var accountType = root.GetProperty("AccountType").GetString();

                BankAccount account = accountType switch
                {
                    "Saving" => JsonSerializer.Deserialize<SavingAccount>(root.GetRawText(), options),
                    "Checking" => JsonSerializer.Deserialize<CheckingAccount>(root.GetRawText(), options),
                    _ => throw new JsonException("Unknown account type.")
                };

                return account;
            }
        }

        public override void Write(Utf8JsonWriter writer, BankAccount value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}

namespace Services
{
    using Entities;

public class BankService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "http://localhost:3000/accounts";
    private readonly JsonSerializerOptions _jsonOptions;

    public BankService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter(), new BankAccountConverter() }
        };
    }

    public async Task<BankAccount> CreateAccount(BankAccount bankAccount)
    {
        var json = JsonSerializer.Serialize(bankAccount, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_baseUrl, content);

        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<BankAccount>(responseJson, _jsonOptions);
    }

    public async Task<BankAccount> GetBalanceAccount(string accountNumber)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}?AccountNumber={accountNumber}");

        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        List<BankAccount> accounts = JsonSerializer.Deserialize<List<BankAccount>>(json, _jsonOptions);
        var account = accounts?.FirstOrDefault();

        // Si es una cuenta Checking, sumamos el OverdraftAmount
        if (account is CheckingAccount checkingAccount)
        {
            checkingAccount.BalanceAmount += checkingAccount.OverdraftAmount;
        }

        return account;
    }


    public async Task<BankAccount> DepositAmount(string accountNumber, decimal amount)
    {
        var account = await GetBalanceAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found. Deposit failed.");
            return null;
        }

        account.Deposit(amount);
        return await UpdateAccount(account);
    }

    public async Task<BankAccount> WithdrawalAmount(string accountNumber, decimal amount)
    {
        var account = await GetBalanceAccount(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found. Withdrawal failed.");
            return null;
        }

        account.Withdrawal(amount);
        return await UpdateAccount(account);
    }

    private async Task<BankAccount> UpdateAccount(BankAccount account)
    {
        var json = JsonSerializer.Serialize(account, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_baseUrl}/{account.id}", content);

        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<BankAccount>(responseJson, _jsonOptions);
    }



}
}
namespace ui
{
    using Entities;
    using Services;

    class Program
    {
        static async Task Main()
        {
            var httpClient = new HttpClient();
            BankService bankService = new BankService(httpClient);
            await Menu(bankService);
        }

        public static async Task Menu(BankService bankService)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Bank Management System ===");
                Console.WriteLine("1 - Create Account");
                Console.WriteLine("2 - Deposit Amount");
                Console.WriteLine("3 - Get Balance");
                Console.WriteLine("4 - Withdraw Amount");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateAccount(bankService);
                        break;
                    case "2":
                        await DepositAmount(bankService);
                        break;
                    case "3":
                        await GetBalance(bankService);
                        break;
                    case "4":
                        await WithdrawalAmount(bankService);
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

        public static async Task CreateAccount(BankService bankService)
        {
            AccountType accountType = GetValidAccountType();
            string accountNumber = GetValidAccountNumber();
            string accountOwner = GetValidAccountOwner();
            decimal initialBalance = GetValidAmount("Initial Balance");

            BankAccount newAccount;

            if (accountType == AccountType.Checking)
            {
                decimal overdraftAmount = 1000000;


                newAccount = new CheckingAccount
                {
                    id = accountNumber,              
                    AccountNumber = accountNumber,
                    AccountOwner = accountOwner,
                    BalanceAmount = initialBalance,
                    AccountType = accountType,
                    OverdraftAmount = overdraftAmount
                };
            }
            else
            {
                newAccount = new SavingAccount
                {
                    id = accountNumber,              
                    AccountNumber = accountNumber,
                    AccountOwner = accountOwner,
                    BalanceAmount = initialBalance,
                    AccountType = accountType
                };
            }

            var createdAccount = await bankService.CreateAccount(newAccount);
            if (createdAccount != null)
            {
                Console.WriteLine("Account created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create account.");
            }
        }


        public static async Task DepositAmount(BankService bankService)
        {
            string accountNumber = GetValidAccountNumber();
            decimal amount = GetValidAmount("Deposit Amount");
            var account = await bankService.DepositAmount(accountNumber, amount);
            if (account != null)
            {
                Console.WriteLine("Amount deposited successfully.");
            }
            else
            {
                Console.WriteLine("Deposit failed. Account not found or other error occurred.");
            }
        }

        public static async Task WithdrawalAmount(BankService bankService)
        {
            string accountNumber = GetValidAccountNumber();
            decimal amount = GetValidAmount("Withdrawal Amount");

            try
            {
                var account = await bankService.WithdrawalAmount(accountNumber, amount);
                if (account != null)
                {
                    Console.WriteLine("Amount withdrawn successfully.");
                }
                else
                {
                    Console.WriteLine("Withdrawal failed. Account not found or other error occurred.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        public static async Task GetBalance(BankService bankService)
        {
            string accountNumber = GetValidAccountNumber();
            BankAccount account = await bankService.GetBalanceAccount(accountNumber);
            if (account != null)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber},\n Account Type: {account.AccountType},\n Account Owner: {account.AccountOwner},\n Balance: {account.BalanceAmount}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
        public static bool IsValidAccountNumber(string accountNumber)
        {
            return !string.IsNullOrWhiteSpace(accountNumber) && accountNumber.Length == 10 && accountNumber.All(char.IsDigit);
        }

        public static bool IsValidAmount(string inputAmount)
        {
            return decimal.TryParse(inputAmount, out decimal amount) && amount > 0;
        }

        public static bool IsValidAccountOwner(string accountOwner)
        {
            return !string.IsNullOrWhiteSpace(accountOwner);
        }

        public static AccountType GetValidAccountType()
        {
            while (true)
            {
                Console.Write("Enter account type (1 - Savings, 2 - Checking): ");
                string input = Console.ReadLine();

                if (input == "1") return AccountType.Saving;
                if (input == "2") return AccountType.Checking;

                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }
        }

        public static string GetValidAccountNumber()
        {
            while (true)
            {
                Console.Write("Enter account number: ");
                string accountNumber = Console.ReadLine();

                if (IsValidAccountNumber(accountNumber))
                    return accountNumber;

                Console.WriteLine("Invalid account number. It must be exactly 10 digits.");
            }
        }

        public static string GetValidAccountOwner()
        {
            while (true)
            {
                Console.Write("Enter account owner: ");
                string accountOwner = Console.ReadLine();

                if (IsValidAccountOwner(accountOwner))
                    return accountOwner;

                Console.WriteLine("Invalid owner name. It cannot be empty.");
            }
        }

        public static decimal GetValidAmount(string titleAmount)
        {
            while (true)
            {
                Console.Write($"Enter {titleAmount}: ");
                string inputAmount = Console.ReadLine();

                if (IsValidAmount(inputAmount))
                    return decimal.Parse(inputAmount);

                Console.WriteLine("Invalid amount. Enter a positive number.");
            }
        }

        public static BankAccount CreateBankAccount(AccountType accountType, string accountNumber, string accountOwner, decimal initialBalance)
        {
            if (accountType == AccountType.Checking)
            {
                Console.Write("Enter overdraft amount: ");
                decimal overdraftAmount = GetValidAmount("Overdraft Amount");

                return new CheckingAccount
                {
                    AccountNumber = accountNumber,
                    AccountOwner = accountOwner,
                    BalanceAmount = initialBalance,
                    AccountType = accountType,
                    OverdraftAmount = overdraftAmount
                };
            }
            else
            {
                return new SavingAccount
                {
                    AccountNumber = accountNumber,
                    AccountOwner = accountOwner,
                    BalanceAmount = initialBalance,
                    AccountType = accountType
                };
            }
        }
    }
}