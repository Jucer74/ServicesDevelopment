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
        private const string BaseUrl = "http://localhost:3000/accounts";

        public BankService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<BankAccount> CreateAccount(BankAccount bankAccount)
        {
            string json = JsonSerializer.Serialize(bankAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la solicitud: {response.StatusCode} - {error}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BankAccount>(responseJson) ?? throw new InvalidOperationException("Error al crear la cuenta.");
        }

        public async Task<BankAccount?> GetAccount(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}?AccountNumber={accountNumber}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<BankAccount>>(json);
            return accounts?.FirstOrDefault();
        }

        public async Task<BankAccount> GetBalanceAccount(string accountNumber)
        {
            var account = await GetAccount(accountNumber);
            return account ?? throw new InvalidOperationException($"Account: {accountNumber} doesn't exist.");
        }

        public async Task<BankAccount> DepositAmount(string accountNumber, decimal amountValue)
        {
            var account = await GetAccount(accountNumber);
            if (account == null) throw new InvalidOperationException($"Account: {accountNumber} doesn't exist.");

            account.Deposit(amountValue);
            return await UpdateAccount(account);
        }

        public async Task<BankAccount> WithdrawalAmount(string accountNumber, decimal amountValue)
        {
            var account = await GetAccount(accountNumber);
            if (account == null) throw new InvalidOperationException($"Account: {accountNumber} doesn't exist.");

            account.Withdrawal(amountValue);
            return await UpdateAccount(account);
        }

        private async Task<BankAccount> UpdateAccount(BankAccount account)
        {
            if (account.id == 0) throw new InvalidOperationException($"Account {account.AccountNumber} doesn't exist.");

            string json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{account.id}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error en la actualización: {response.StatusCode} - {error}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BankAccount>(responseJson) ?? throw new InvalidOperationException("Error al actualizar la cuenta.");
        }
    }
}
