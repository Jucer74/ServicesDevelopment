using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities.Models;
using BankApp.Entities.Enum;

namespace BankApp.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:3000/accounts";

        public BankService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<BankAccount?> CreateAccount(BankAccount account)
        {
            if (await ExistsAccount(account.AccountNumber))
            {
                return null;
            }

            var jsonContent = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return account;
        }

        public async Task<BankAccount?> GetBalance(string accountNumber)
        {
            var account = await GetAccountByNumber(accountNumber);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<BankAccount> DepositAmount(string accountNumber, decimal amount)
        {
            var account = await GetAccountByNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("La cuenta no existe.");
                return null;
            }

            account.Deposit(amount);
            await UpdateAccount(account);
            return account;
        }

        public async Task<BankAccount?> WithdrawalAmount(string accountNumber, decimal amount)
        {
            var account = await GetAccountByNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Error: La cuenta no existe.");
                return null;
            }

            if (account.AccountType == AccountType.Checking && account.BalanceAmount < amount)
            {
                Console.WriteLine("Error: Fondos insuficientes.");
                return null;
            }

            account.Withdrawal(amount);
            bool code = await UpdateAccount(account);
            if (code)
            {
                return account;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ExistsAccount(string accountNumber)
        {
            var account = await GetAccountByNumber(accountNumber);
            return account != null;
        }

        private async Task<BankAccount?> GetAccountByNumber(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}?AccountNumber={accountNumber}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<BankAccount>>(jsonResponse);
            return accounts?.Count > 0 ? accounts[0] : null;
        }

        private async Task<bool> UpdateAccount(BankAccount account)
        {
            // 1️⃣ Obtén el ID real desde el JSON
            var getResponse = await _httpClient.GetAsync($"{_apiUrl}?AccountNumber={account.AccountNumber}");
            if (!getResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return false;
            }

            var jsonResponse = await getResponse.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonResponse);
            var root = doc.RootElement;

            if (!root.EnumerateArray().Any()) // Si no hay cuentas
            {
                Console.WriteLine("Cuenta no encontrada.");
                return false;
            }

            var firstAccount = root.EnumerateArray().First();
            string? accountId = firstAccount.GetProperty("id").GetString(); // Extrae el ID del JSON

            // 2️⃣ Serializa el objeto actualizado (sin `id`)
            var jsonContent = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");

            // 3️⃣ Actualiza usando `id` en la URL
            var response = await _httpClient.PatchAsync($"{_apiUrl}/{accountId}", jsonContent);

            return response.IsSuccessStatusCode;
        }


    }
}