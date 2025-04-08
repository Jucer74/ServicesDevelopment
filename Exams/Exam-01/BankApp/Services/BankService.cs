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
                int newId = await GetNextAccountIdAsync();

                var newAccount = new
                {
                    id = newId,  // ID secuencial
                    AccountNumber = account.AccountNumber,
                    AccountOwner = account.AccountOwner,
                    BalanceAmount = account.BalanceAmount,
                    AccountType = account.AccountType
                };

                var json = JsonSerializer.Serialize(newAccount);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var postResponse = await _httpClient.PostAsync("accounts", content);

                if (!postResponse.IsSuccessStatusCode)
                    Console.WriteLine($"Warning: Failed to save account to API. Server Response: {await postResponse.Content.ReadAsStringAsync()}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: Unable to connect to the API. Make sure json-server is running.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }

        private async Task<int> GetNextAccountIdAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("accounts");
                var existingAccounts = JsonSerializer.Deserialize<List<JsonElement>>(response) ?? new List<JsonElement>();

                return existingAccounts.Count > 0
                    ? existingAccounts.Max(a =>
                        a.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.Number
                            ? idProp.GetInt32()
                            : idProp.ValueKind == JsonValueKind.String && int.TryParse(idProp.GetString(), out int parsedId)
                                ? parsedId
                                : 0) + 1
                    : 1;
            }
            catch
            {
                return 1; // Si falla, comienza con ID = 1
            }
        }

        private async Task SaveAccountsAsync()
        {
            try
            {
                foreach (var account in accounts)
                {
                    var json = JsonSerializer.Serialize(account);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    int id = await GetAccountIdAsync(account.AccountNumber);
                    if (id == 0)
                    {
                        Console.WriteLine($"Warning: Invalid ID for account {account.AccountNumber}.");
                        continue;
                    }

                    var response = await _httpClient.PutAsync($"accounts/{id}", content);

                    if (!response.IsSuccessStatusCode)
                        Console.WriteLine($"Warning: Failed to update account {account.AccountNumber} in API. Server Response: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: Unable to connect to the API. Make sure json-server is running.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }

        private async Task<int> GetAccountIdAsync(string accountNumber)
        {
            try
            {
                var getResponse = await _httpClient.GetStringAsync($"accounts?AccountNumber={accountNumber}");
                var existingAccounts = JsonSerializer.Deserialize<List<JsonElement>>(getResponse) ?? new List<JsonElement>();

                if (existingAccounts.Count == 0) return 0;

                var idElement = existingAccounts[0].GetProperty("id");

                return idElement.ValueKind == JsonValueKind.Number
                    ? idElement.GetInt32()
                    : int.TryParse(idElement.GetString(), out int parsedId) ? parsedId : 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task LoadAccountsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("accounts");
                var loadedAccounts = JsonSerializer.Deserialize<List<JsonElement>>(response);

                if (loadedAccounts != null)
                {
                    foreach (var accountJson in loadedAccounts)
                    {
                        var accountType = accountJson.GetProperty("AccountType").GetInt32();
                        BankAccount account = accountType switch
                        {
                            1 => JsonSerializer.Deserialize<SavingAccount>(accountJson.GetRawText()),
                            2 => JsonSerializer.Deserialize<CheckingAccount>(accountJson.GetRawText()),
                            _ => throw new Exception("Unknown account type")
                        };
                        accounts.Add(account);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Warning: Could not load accounts from API.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }
    }
}
