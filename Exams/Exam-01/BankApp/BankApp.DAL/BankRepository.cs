using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.DAL;

public class BankRepository
{
    private readonly HttpClient _client = new();
    private const string ApiUrl = "http://localhost:3000/BankAccounts";

    public async Task<List<IBankAccount>> GetAccountsAsync()
    {
        var response = await _client.GetStringAsync(ApiUrl);
        return JsonSerializer.Deserialize<List<IBankAccount>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<IBankAccount>();
    }

    public async Task<string> AddAccountAsync(IBankAccount account)
    {
        var content = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(ApiUrl, content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UpdateAccountAsync(string accountNumber, IBankAccount account)
    {
        var content = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"{ApiUrl}/{accountNumber}", content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> DeleteAccountAsync(string accountNumber)
    {
        var response = await _client.DeleteAsync($"{ApiUrl}/{accountNumber}");
        return await response.Content.ReadAsStringAsync();
    }
}
