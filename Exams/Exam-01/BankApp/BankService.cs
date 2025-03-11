using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace BankApp
{
    public class BankService
    {
        private HttpClient _client = new HttpClient();
        private string _urlBase = "";
        public BankService()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            _urlBase = config["baseUrl"];
        }

        public async Task<BankAccount> CreateAccount(BankAccount bankAccount)
        {
            string jsonBankAccount = JsonConvert.SerializeObject(bankAccount);
            var httpContent = new StringContent(jsonBankAccount, Encoding.UTF8, "application/json");
            var responsePost = await _client.PostAsync(_urlBase, httpContent);
            var returnPost = await responsePost.Content.ReadAsStringAsync();
            var bankAccountCreated = JsonConvert.DeserializeObject<BankAccount>(returnPost);
            return bankAccountCreated;
        }

        public async Task<BankAccount> GetBalanceAccount(string accountNumber)
        {
            var account = await GetAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found. Please check the account number.");
                return null;
            }

            return account;
        }

        public async Task<BankAccount> DepositAmount(string accountNumber, decimal amountValue)
        {
            var account = await GetAccount(accountNumber);
            account.Deposit(amountValue);
            string jsonBankAccount = JsonConvert.SerializeObject(account);
            var httpContent = new StringContent(jsonBankAccount, Encoding.UTF8, "application/json");
            var responsePut = await _client.PutAsync(_urlBase + "/" + account.Id, httpContent);
            var returnPut = await responsePut.Content.ReadAsStringAsync();
            var accountUpdated = JsonConvert.DeserializeObject<BankAccount>(returnPut);
            return accountUpdated;
        }

        public async Task<BankAccount> WithdrawalAmount(string accountNumber, decimal amountValue)
        {

            var account = await GetAccount(accountNumber);
            try
            {
                account.Withdrawal(amountValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return account;
            }
            

            string jsonBankAccount = JsonConvert.SerializeObject(account);
            var httpContent = new StringContent(jsonBankAccount, Encoding.UTF8, "application/json");
            var responsePut = await _client.PutAsync(_urlBase + "/" + account.Id, httpContent);
            var returnPut = await responsePut.Content.ReadAsStringAsync();
            var accountUpdated = JsonConvert.DeserializeObject<BankAccount>(returnPut);
            return accountUpdated;
        }

        private async Task<BankAccount> GetAccount(string accountNumber)
        {
            var request = await _client.GetAsync(_urlBase + "?account_number=" + accountNumber);
            var response = await request.Content.ReadAsStringAsync();
            var bankAccount = JsonConvert.DeserializeObject<List<BankAccount>>(response);
            return bankAccount.First();

            // try
            // {
            //     var request = await _client.GetAsync(_urlBase + "?account_number=" + accountNumber);

            //     if (!request.IsSuccessStatusCode)
            //     {
            //         throw new Exception($"Error retrieving account. Status code: {request.StatusCode}");
            //     }

            //     var response = await request.Content.ReadAsStringAsync();
            //     var bankAccountList = JsonConvert.DeserializeObject<List<BankAccount>>(response);

            //     if (bankAccountList == null || !bankAccountList.Any())
            //     {
            //         throw new Exception($"No account found with account number {accountNumber}");
            //     }

            //     return bankAccountList.First();
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"An error occurred: {ex.Message}");
            //     return null;
            // }


        }

        public async Task<int> GetLastAccountId()
        {
            int newId;
            var response = await _client.GetAsync(_urlBase);
            if (!response.IsSuccessStatusCode)
            {
                return 1;
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<List<BankAccount>>(jsonString);
            if (accounts.Any())
            {
                var lastId = accounts.Max(a => a.Id);
                newId = lastId + 1;
            }
            else
            {
                newId = 1;
            }

            return newId;
        }

    }
}