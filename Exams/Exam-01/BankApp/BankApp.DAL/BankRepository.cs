﻿using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BankApp.Entities;

namespace BankApp.DAL
{
    public class BankRepository
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/accounts";

        public BankRepository()
        {
            _httpClient = new HttpClient();
        }

    public async Task<List<IBankAccount>> GetAllAccountsAsync()
{
    var response = await _httpClient.GetStringAsync(ApiUrl);
    var jsonElements = JsonSerializer.Deserialize<List<JsonElement>>(response) ?? new List<JsonElement>();

    return jsonElements.Select(BankAccountFactory.FromJson).ToList();
}

        public async Task<IBankAccount> GetAccountByNumberAsync(string accountNumber)
        {
            var accounts = await GetAllAccountsAsync();
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public async Task CreateAccountAsync(IBankAccount account)
{
    var randomId = Guid.NewGuid().ToString("N")[..4]; // Genera un ID aleatorio de 4 caracteres
    var json = JsonSerializer.Serialize(new
    {
        id = randomId,
        account.AccountNumber,
        account.AccountOwner,
        account.BalanceAmount,
        account.AccountType,
        OverdraftAmount = account is CheckingAccount checkingAcc ? checkingAcc.OverdraftAmount : (decimal?)null
    });

    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var response = await _httpClient.PostAsync(ApiUrl, content);

    if (!response.IsSuccessStatusCode)
    {
        throw new InvalidOperationException("Failed to create account.");
    }
}

        public async Task UpdateAccountAsync(IBankAccount account)
{
    var json = JsonSerializer.Serialize(account);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var response = await _httpClient.PutAsync($"{ApiUrl}/{account.AccountNumber}", content);

    if (!response.IsSuccessStatusCode)
    {
        throw new InvalidOperationException("Failed to update account.");
    }
}

    }
}
