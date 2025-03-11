using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;

namespace BankApp.Services
{
    public class BankService
    {
        private readonly List<BankAccount> accounts = new();
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:3000/") };

        public BankService()
        {
            LoadAccountsAsync().Wait(); // Carga las cuentas al iniciar
        }

        public BankAccount CreateAccount(string accountNumber, string accountOwner, decimal balanceAmount, AccountType accountType)
        {
            if (accounts.Any(acc => acc.AccountNumber == accountNumber))
                throw new InvalidOperationException($"Account {accountNumber} already exists");

            BankAccount account = accountType switch
            {
                AccountType.Saving => new SavingAccount(accountNumber, accountOwner, balanceAmount),
                AccountType.Checking => new CheckingAccount(accountNumber, accountOwner, balanceAmount),
                _ => throw new ArgumentException("Invalid account type")
            };
            accounts.Add(account);
            SaveAccountAsync(account).Wait(); // Guarda en JSON Server
            return account;
        }

        public BankAccount GetBalance(string accountNumber)
        {
            return accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber) ??
                   throw new KeyNotFoundException($"Account {accountNumber} doesn't exist");
        }

        public void DepositAccount(string accountNumber, decimal amount)
        {
            GetBalance(accountNumber).Deposit(amount);
            SaveAccountsAsync().Wait(); // Guarda cambios en JSON Server
        }

        public bool WithdrawalAccount(string accountNumber, decimal amount)
        {
            var success = GetBalance(accountNumber).Withdrawal(amount);
            if (success)
                SaveAccountsAsync().Wait(); // Guarda cambios en JSON Server
            return success;
        }

        private async Task SaveAccountAsync(BankAccount account)
        {
            try
            {
                var json = JsonSerializer.Serialize(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("accounts", content);

                if (!response.IsSuccessStatusCode)
                    Console.WriteLine(" Warning: Failed to save account to API.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(" Error: Unable to connect to the API. Make sure json-server is running.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }

        private async Task SaveAccountsAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(accounts);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("accounts", content);
                
                if (!response.IsSuccessStatusCode)
                    Console.WriteLine(" Warning: Failed to update accounts in API.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(" Error: Unable to connect to the API. Make sure json-server is running.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }

        private async Task LoadAccountsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("accounts");
                var loadedAccounts = JsonSerializer.Deserialize<List<BankAccount>>(response);
                if (loadedAccounts != null)
                    accounts.AddRange(loadedAccounts);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(" Warning: Could not load accounts from API.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }
    }
}
