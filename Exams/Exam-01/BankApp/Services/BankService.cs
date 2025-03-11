using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entitites;
namespace BankApp.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:3000/accounts";
          public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
         public async Task<BankAccount> CreateAccountAsync(BankAccount newAccount)
        {
            var json = JsonSerializer.Serialize(newAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
           
            var account = JsonSerializer.Deserialize<BankAccount>(json);

            if (account == null)
                throw new Exception("Error al deserializar la cuenta.");
            return JsonSerializer.Deserialize<BankAccount>(responseData);
        }

        public async Task<BankAccount> GetAccountAsync(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{accountNumber}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Cuenta no encontrada.");

            var json = await response.Content.ReadAsStringAsync();
            if (account == null)
            throw new Exception("Error al deserializar la cuenta.");
            return JsonSerializer.Deserialize<BankAccount>(json);
        }
         public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountAsync(accountNumber);
            account.BalanceAmount += amount;

            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            await _httpClient.PutAsync($"{_baseUrl}/{accountNumber}", content);
        }


        public async Task WithdrawalAsync(string accountNumber, decimal amountValue)
        {
            var account = await GetAccountAsync(accountNumber);
            if (account.BalanceAmount + account.OverdraftAmount < amountValue)
                throw new InvalidOperationException("Fondos insuficientes.");

            account.BalanceAmount -= amountValue;
            
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"{_baseUrl}/{accountNumber}", content);
        }
    }
    }