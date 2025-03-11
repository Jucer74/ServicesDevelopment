using System.Text;
using Models;
using Newtonsoft.Json;
using System.Net;


namespace BankServices {
    public class BankService 
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/api/v1";

        public BankService()
        {
            _httpClient = new HttpClient(); 
        }

        public class AccountNotFoundException : Exception
        {
        public AccountNotFoundException(string message) : base(message) { }
        }

        public async Task<BankAccount> GetAccount(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/accounts/{accountNumber}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new AccountNotFoundException("Account does not exist.");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Server Error.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<BankAccount>(json);

            if (account != null)
            {
                return account;
            }

            throw new AccountNotFoundException("Account does not exist.");
        }
        public async Task<BankAccount> CreateAccount(BankAccount bankAccount)
        {
            try
            {
                await GetAccount(bankAccount.AccountNumber);
                throw new Exception("Account already exists!");
            }
            catch (AccountNotFoundException)
            {
                string json = JsonConvert.SerializeObject(bankAccount);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{ApiUrl}/accounts", content);
                response.EnsureSuccessStatusCode();

                return bankAccount;
            }
        }

        public async Task<BankAccount> GetBalance(string accountNumber)
        {
            try {
                var account = await GetAccount(accountNumber);
                return account;
            } catch (AccountNotFoundException)
            {
                throw new Exception("Account does not exist.");
            }
        }
        public async Task<BankAccount> DepositAmount(string accountNumber, decimal amountValue)
        {
            try
            {
                var account = await GetAccount(accountNumber);
                
                account.BalanceAmount += amountValue;
                string json = JsonConvert.SerializeObject(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{ApiUrl}/accounts/{accountNumber}", content);
                response.EnsureSuccessStatusCode();

                return account;
            }
            catch (AccountNotFoundException)
            {
                throw new Exception("Account does not exist.");
            }
        }
        public async Task<BankAccount> WithdrawalAccount(string accountNumber, decimal amount) 
        {
            try
            {
                var account = await GetAccount(accountNumber);
            
                if (account.AccountType == 0 || account.BalanceAmount >= amount )
                {
                    account.BalanceAmount -= amount;
                    string json = JsonConvert.SerializeObject(account);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"{ApiUrl}/accounts/{accountNumber}", content);
                    response.EnsureSuccessStatusCode();
                } 
                else 
                {
                    throw new Exception("Insufficient funds.");
                }
                
                return account;
            } 
            catch (AccountNotFoundException)
            {
                throw new Exception("Account does not exist.");
            }
        }
    }

}

