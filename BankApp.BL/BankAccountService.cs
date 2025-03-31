using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BankApp.Entities;

namespace BankApp.BL
{
    public class BankAccountRepository
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/accounts";

        public BankAccountRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAccountAsync(BankAccount account)
        {
            var content = new StringContent(JsonSerializer.Serialize(account), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Failed to create account. Server response: {errorResponse}");
            }
        }

        public async Task<BankAccount?> GetAccountAsync(string accountNumber)
        {
            var response = await _httpClient.GetAsync(ApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response from server: {responseData}"); 

                var accounts = JsonSerializer.Deserialize<List<BankAccount>>(responseData);

                return accounts?.FirstOrDefault(a => a.AccountNumber == accountNumber);
            }
            Console.WriteLine($"Failed to retrieve accounts. Status code: {response.StatusCode}"); 
            return null;
        }

        public async Task UpdateAccountAsync(BankAccount account)
        {
            var existingAccount = await GetAccountAsync(account.AccountNumber);
            if (existingAccount == null)
            {
                throw new InvalidOperationException("Account not found.");
            }

            var url = $"{ApiUrl}/{existingAccount.Id}";
            Console.WriteLine($"Updating account at URL: {url}");

            var content = new StringContent(JsonSerializer.Serialize(account), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
  public async Task DepositAsync(string accountNumber, decimal amount)
{
    Console.WriteLine($"🔹 Intentando depositar {amount} en la cuenta {accountNumber}");

    var account = await GetAccountAsync(accountNumber);
    if (account == null)
    {
        Console.WriteLine("ERROR: Cuenta no encontrada. No se puede depositar.");
        throw new Exception("Cuenta no encontrada");
    }

    Console.WriteLine($" Saldo actual antes del depósito: {account.Balance}");
    account.Balance += amount;
    Console.WriteLine($"Nuevo saldo después del depósito: {account.Balance}");

    await UpdateAccountAsync(account);
    Console.WriteLine("Depósito realizado exitosamente.");

    var updatedAccount = await GetAccountAsync(accountNumber);
    Console.WriteLine($"Saldo final en la API: {updatedAccount?.Balance}");
}


    }
}