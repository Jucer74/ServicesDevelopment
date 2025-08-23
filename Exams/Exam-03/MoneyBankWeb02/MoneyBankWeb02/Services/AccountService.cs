using System.Net.Http.Json;
using MoneyBankWeb02.Models;

namespace MoneyBankWeb02.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("MoneyBankApi");
        }

        public async Task<List<AccountDto>> GetAccountsAsync()
        {
            var accounts = await _httpClient.GetFromJsonAsync<List<AccountDto>>("accounts");
            return accounts ?? new List<AccountDto>();
        }
    }
}
