using System;
using System.Threading.Tasks;
using System.Text.Json;

using BankApp.DAL;
using BankApp.Entities;

namespace BankApp.BL
{
    public class BankService
    {
        private readonly BankRepository _repository;
        private readonly HttpClient _httpClient = new HttpClient();
private const string ApiUrl = "http://localhost:3000/accounts";



        public BankService()
        {
            _repository = new BankRepository();
        }

        public async Task CreateAccountAsync(IBankAccount account)
        {
            var existingAccount = await _repository.GetAccountByNumberAsync(account.AccountNumber);
            if (existingAccount != null)
                throw new InvalidOperationException("Account already exists.");
            
            await _repository.CreateAccountAsync(account);
        }

        public async Task<decimal> GetBalanceAsync(string accountNumber)
{
    Console.WriteLine($"Searching account: {accountNumber}");

    var response = await _httpClient.GetStringAsync(ApiUrl);
    Console.WriteLine($"API Response: {response}");

    if (string.IsNullOrEmpty(response))
    {
        Console.WriteLine("Error: No data received from API.");
        throw new InvalidOperationException("No accounts found.");
    }

    // Deserializar la respuesta en una lista genérica
    var rawAccounts = JsonSerializer.Deserialize<List<JsonElement>>(response);
    var allAccounts = new List<IBankAccount>();

    foreach (var element in rawAccounts)
    {
        var accountType = element.GetProperty("accountType").GetString();

        if (accountType == "Saving")
        {
            var savingAcc = JsonSerializer.Deserialize<SavingAccount>(element.GetRawText()); // 🔹 Renombrado
            allAccounts.Add(savingAcc);
        }
        else if (accountType == "Checking")
        {
            var checkingAcc = JsonSerializer.Deserialize<CheckingAccount>(element.GetRawText()); // 🔹 Renombrado
            allAccounts.Add(checkingAcc);
        }
    }

    var foundAccount = allAccounts.FirstOrDefault(a => a.AccountNumber.Trim() == accountNumber.Trim());

    if (foundAccount == null)
    {
        Console.WriteLine("Error: Account not found.");
        throw new InvalidOperationException("Account not found.");
    }

    return foundAccount.BalanceAmount;
}

        public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await _repository.GetAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new InvalidOperationException("Account not found.");

            account.Deposit(amount);
            await _repository.UpdateAccountAsync(account);
        }

        public async Task WithdrawalAsync(string accountNumber, decimal amount)
        {
            var account = await _repository.GetAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new InvalidOperationException("Account not found.");

            account.Withdrawal(amount);
            await _repository.UpdateAccountAsync(account);
        }
    }
}
