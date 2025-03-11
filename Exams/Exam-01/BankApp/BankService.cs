using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class BankService
{
    private const string API_URL = "http://localhost:3000/accounts";
    private static readonly HttpClient client = new HttpClient();

    public async Task CreateAccount(IBankAccount account)
    {
        var json = JsonSerializer.Serialize(account);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(API_URL, content);

        if (!response.IsSuccessStatusCode)
            Console.WriteLine("Error al crear la cuenta.");
        else
            Console.WriteLine("Cuenta creada exitosamente.");
    }

    public async Task GetBalance(string accountNumber)
    {
        var response = await client.GetAsync($"{API_URL}/{accountNumber}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("La cuenta no existe.");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var account = JsonSerializer.Deserialize<SavingAccount>(json);
        Console.WriteLine($"Saldo actual de la cuenta {accountNumber}: {account.BalanceAmount}");
    }

    public async Task DepositAccount(string accountNumber, decimal amount)
    {
        var response = await client.GetAsync($"{API_URL}/{accountNumber}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("La cuenta no existe.");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var account = JsonSerializer.Deserialize<SavingAccount>(json);
        account.Deposit(amount);

        await UpdateAccount(account);
        Console.WriteLine($"Depósito exitoso. Nuevo saldo: {account.BalanceAmount}");
    }

    public async Task WithdrawalAccount(string accountNumber, decimal amount)
    {
        var response = await client.GetAsync($"{API_URL}/{accountNumber}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("La cuenta no existe.");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var account = JsonSerializer.Deserialize<SavingAccount>(json);
        account.Withdrawal(amount);

        await UpdateAccount(account);
        Console.WriteLine($"Retiro exitoso. Nuevo saldo: {account.BalanceAmount}");
    }

    private async Task UpdateAccount(IBankAccount account)
    {
        var json = JsonSerializer.Serialize(account);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await client.PutAsync($"{API_URL}/{account.AccountNumber}", content);
    }
    private async Task<IBankAccount> FindAccount(string accountNumber)  
    {
    var response = await client.GetAsync($"{API_URL}?AccountNumber={accountNumber}");
    if (!response.IsSuccessStatusCode) return null;

    var json = await response.Content.ReadAsStringAsync();
    var accounts = JsonSerializer.Deserialize<List<SavingAccount>>(json);

    return accounts.Count > 0 ? accounts[0] : null;
    }

}
