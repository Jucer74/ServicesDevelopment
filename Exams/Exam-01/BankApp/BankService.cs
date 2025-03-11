using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace BankApp
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5000/accounts"; // URL del JSON-Server

        public BankService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<BankAccount> CreateAccount(BankAccount bankAccount)
        {
            string json = JsonSerializer.Serialize(bankAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);
            response.EnsureSuccessStatusCode();

            return bankAccount; // Return the created account
        }

        public async Task<BankAccount> GetBalanceAccount(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}?accountNumber={accountNumber}");
            if (!response.IsSuccessStatusCode) 
                throw new InvalidOperationException("Cuenta no encontrada.");

            var json = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<BankAccount>>(json);

            return accounts?.FirstOrDefault() ?? throw new InvalidOperationException("Cuenta no encontrada.");
        }


        public async Task<BankAccount> DepositAmount(string accountNumber, decimal amount)
        {
            var account = await GetAccount(accountNumber);
            account.Deposit(amount);
            await UpdateAccount(account);
            return account; // Retorn the updated account
        }

        public async Task<BankAccount> WithdrawalAmount(string accountNumber, decimal amount)
        {
            var account = await GetAccount(accountNumber);
            account.Withdrawal(amount);
            await UpdateAccount(account);
            return account; // Retorn the updated account
        }
    }
}
