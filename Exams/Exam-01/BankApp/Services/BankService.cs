using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BankApp.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public BankService()
        {
            _httpClient = new HttpClient();

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "UI")) 
                .AddJsonFile("appsettings.json")
                .Build();

            _baseUrl = config["JsonServer:BaseUrl"];
        }

     
        public async Task<List<BankAccount>> GetAccountsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}/accounts");
            return JsonSerializer.Deserialize<List<BankAccount>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> AccountExistsByIdAsync(string accountId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/accounts/{accountId}");
            return response.IsSuccessStatusCode;
        }


      
        public async Task<bool> CreateAccountAsync(BankAccount bankAccount)
        {
            bankAccount.id = bankAccount.AccountNumber;

            if (await AccountExistsByIdAsync(bankAccount.id))
            {
                Console.WriteLine("La cuenta ya existe.");
                return false;
            }

            var json = JsonSerializer.Serialize(bankAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/accounts", content);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al crear la cuenta: {response.StatusCode} - {errorMessage}");
                return false;
            }

            return true;
        }

      
        public async Task<BankAccount?> GetAccountByNumberAsync(string accountNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/accounts/{accountNumber}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener cuenta o no ha sido creada previamente: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BankAccount>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al obtener cuenta: {ex.Message}");
                return null;
            }
        }

       
        public async Task<BankAccount?> GetBalanceAsync(string accountNumber)
        {
            var account = await GetAccountByNumberAsync(accountNumber);
            if (account == null)
                Console.WriteLine("Cuenta no encontrada.");

            return account;
        }

     
        public async Task<bool> DepositAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountByNumberAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return false;
            }

            account.BalanceAmount += amount;
            Console.WriteLine("Deposito de "+ amount + " completado con exito " + "nuevo saldo: " + account.BalanceAmount);
            return await UpdateAccountAsync(account);
        }

    
        public async Task<bool> WithdrawalAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountByNumberAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return false;
            }

            if (account.AccountType == AccountType.Checking && (account.BalanceAmount - amount < -account.OverdraftAmount))
            {
                Console.WriteLine("Excede el límite de sobregiro.");
                return false;
            }

            if (account.BalanceAmount < amount)
            {
                Console.WriteLine("Fondos insuficientes.");
                return false;
            }

            account.BalanceAmount -= amount;
            return await UpdateAccountAsync(account);
        }

       
        public async Task<bool> UpdateAccountAsync(BankAccount account)
        {
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/accounts/{account.AccountNumber}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
