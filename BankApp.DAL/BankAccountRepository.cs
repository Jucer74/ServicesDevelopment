using BankApp.Entities;
using System.Text.Json;

namespace BankApp.DAL
{
    public class BankAccountRepository
    {
        private const string FilePath = "accounts.json";

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public BankAccountRepository() { } // ✅ Constructor vacío corregido

        public async Task<List<BankAccount>> GetAllAccountsAsync()
        {
            if (!File.Exists(FilePath))
            {
                return new List<BankAccount>();
            }

            string json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<BankAccount>>(json) ?? new List<BankAccount>();
        }

        public async Task<BankAccount?> GetAccountAsync(string accountNumber)
        {
            var accounts = await GetAllAccountsAsync();
            return accounts.FirstOrDefault(a => 
                !string.IsNullOrEmpty(a.AccountNumber) && a.AccountNumber.Trim() == accountNumber.Trim());
        }

        public async Task AddAccountAsync(BankAccount newAccount) // ✅ Método corregido
        {
            var accounts = await GetAllAccountsAsync();

            if (accounts.Any(a => 
                !string.IsNullOrEmpty(a.AccountNumber) && a.AccountNumber.Trim() == newAccount.AccountNumber.Trim()))
            {
                throw new InvalidOperationException("Account number already exists.");
            }

            accounts.Add(newAccount);
            await SaveAccountsAsync(accounts);
        }

        public async Task UpdateAccountAsync(BankAccount account)
        {
            var accounts = await GetAllAccountsAsync();
            var index = accounts.FindIndex(a => 
                !string.IsNullOrEmpty(a.AccountNumber) && a.AccountNumber.Trim() == account.AccountNumber.Trim());

            if (index == -1)
            {
                throw new InvalidOperationException("Account not found.");
            }

            accounts[index] = account;
            await SaveAccountsAsync(accounts);
        }

        public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountAsync(accountNumber)
                ?? throw new InvalidOperationException("Account not found.");

            if (amount <= 0)
            {
                throw new InvalidOperationException("Deposit amount must be positive.");
            }

            account.BalanceAmount += amount;
            await UpdateAccountAsync(account);
        }

        public async Task WithdrawAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountAsync(accountNumber)
                ?? throw new InvalidOperationException("Account not found.");

            if (amount <= 0)
            {
                throw new InvalidOperationException("Withdrawal amount must be positive.");
            }

            if (account.BalanceAmount < amount)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }

            account.BalanceAmount -= amount;
            await UpdateAccountAsync(account);
        }

        private async Task SaveAccountsAsync(List<BankAccount> accounts)
        {
            try
            {
                string json = JsonSerializer.Serialize(accounts, _jsonOptions);
                await File.WriteAllTextAsync(FilePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving accounts: {ex.Message}");
            }
        }
    }
}
