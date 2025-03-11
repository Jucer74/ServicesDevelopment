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

        public async Task<BankAccount> CreateAccount(BankAccount account)
        {
            if (await ExistsAccount(account.AccountNumber))
            {
                Console.WriteLine("La cuenta ya existe.");
                return null;
            }

            var jsonContent = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error al crear la cuenta.");
                return null;
            }

            Console.WriteLine("Cuenta creada exitosamente.");
            return account;
        }

        public async Task<BankAccount> GetBalance(string accountNumber)
        {
            var account = await GetAccountByNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Error: La cuenta no existe.");
                return null;
            }

            Console.WriteLine($"Saldo actual de la cuenta {accountNumber}: {account.BalanceAmount}");
            return account;
        }

        public async Task<BankAccount> DepositAccount(string accountNumber, decimal amount)
        {
            var account = await GetAccountByNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("La cuenta no existe.");
                return null;
            }

            account.Deposit(amount);
            await UpdateAccount(account);
            Console.WriteLine($"Depósito exitoso. Nuevo saldo: {account.BalanceAmount}");
            return account;
        }

        public async Task<BankAccount> WithdrawalAccount(string accountNumber, decimal amount)
        {
            var account = await GetAccountByNumber(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Error: La cuenta no existe.");
                return null;
            }

            if (account.AccountType == AccountType.Checking && account.BalanceAmount + account.OverdraftAmount < amount)
            {
                Console.WriteLine("Error: Fondos insuficientes incluso con sobregiro.");
                return null;
            }
            else if (account.BalanceAmount < amount)
            {
                Console.WriteLine("Error: Fondos insuficientes.");
                return null;
            }

            account.Withdrawal(amount);
            await UpdateAccount(account);
            Console.WriteLine($"Retiro exitoso. Nuevo saldo: {account.BalanceAmount}");
            return account;
        }

        public async Task<bool> ExistsAccount(string accountNumber)
        {
            var account = await GetAccountByNumber(accountNumber);
            return account != null;
        }

        private async Task<BankAccount> GetAccountByNumber(string accountNumber)
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
            var jsonContent = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}/{account.AccountNumber}", jsonContent);

            return response.IsSuccessStatusCode;
        }
    }
}