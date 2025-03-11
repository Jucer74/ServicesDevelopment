using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;

namespace BL
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public BankService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<BankAccount?> CreateAccountAsync(BankAccount account)
        {
            var existingAccount = await GetBalanceAsync(account.AccountNumber);
            if (existingAccount != null)
            {
                throw new InvalidOperationException($"Account : {account.AccountNumber} already exists.");
            }

            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BankAccount>(responseContent);
        }

        public async Task<BankAccount?> GetBalanceAsync(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{accountNumber}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BankAccount>(content);
        }

        public async Task<BankAccount> DepositAsync(string accountNumber, decimal amount)
        {
            var account = await GetBalanceAsync(accountNumber);
            if (account == null)
            {
                throw new InvalidOperationException("La cuenta no existe.");
            }

            account.Deposit(amount);

            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{account.id}", content);
            response.EnsureSuccessStatusCode();

            return account;
        }

        public async Task<BankAccount> WithdrawalAsync(string accountNumber, decimal amount)
        {
            var account = await GetBalanceAsync(accountNumber);
            if (account == null)
            {
                throw new InvalidOperationException("La cuenta no existe.");
            }

            account.Withdrawal(amount);

            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{account.id}", content);
            response.EnsureSuccessStatusCode();

            return account;
        }
    }
}