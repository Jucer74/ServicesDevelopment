using BankApp.Entities;
using System.Net.Http.Json;


namespace BankApp.BL
{
    public class BankAccountDTO
    {
        public string Type { get; set; }             
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal OverdraftAmount { get; set; }
    }

    public class BankAccountRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:3000/accounts";

        public BankAccountRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
  
        private IBankAccount FromDTO(BankAccountDTO dto)
        {
            if (dto.Type == "Saving")
            { 
                var saving = new SavingAccount(dto.AccountNumber, dto.AccountOwner, dto.BalanceAmount);
                return saving;
            }
            else if (dto.Type == "Checking")
            {
                var initialBalance = dto.BalanceAmount - 1_000_000m;
                if (initialBalance < 0) initialBalance = 0; 

                var checking = new CheckingAccount(dto.AccountNumber, dto.AccountOwner, initialBalance);
                if (dto.OverdraftAmount > 0)
                {
                    checking.Withdrawal(dto.OverdraftAmount);
                }
                return checking;
            }
            else
            {
                throw new ArgumentException($"Invalid type: {dto.Type}");
            }
        }

        private BankAccountDTO ToDTO(IBankAccount account)
        {
            if (account is SavingAccount saving)
            {
                return new BankAccountDTO
                {
                    Type = "Saving",
                    AccountNumber = saving.AccountNumber,
                    AccountOwner = saving.AccountOwner,
                    BalanceAmount = saving.BalanceAmount,
                    OverdraftAmount = 0
                };
            }
            else if (account is CheckingAccount checking)
            {
                return new BankAccountDTO
                {
                    Type = "Checking",
                    AccountNumber = checking.AccountNumber,
                    AccountOwner = checking.AccountOwner,
                    BalanceAmount = checking.BalanceAmount,
                    OverdraftAmount = checking.OverdraftAmount
                };
            }
            else
            {
                throw new ArgumentException("Unknown account type.");
            }
        }
        private IEnumerable<IBankAccount> LoadAll()
        {
            var task = _httpClient.GetFromJsonAsync<List<BankAccountDTO>>(BaseUrl);
            task.Wait();
            var dtos = task.Result ?? new List<BankAccountDTO>();
            return dtos.Select(FromDTO).ToList();
        }

        public bool Exists(string accountNumber)
        {
            return GetByAccountNumber(accountNumber) != null;
        }

        public IBankAccount GetByAccountNumber(string accountNumber)
        {
            var all = LoadAll();
            return all.LastOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void Create(IBankAccount account)
        {
            if (Exists(account.AccountNumber))
                throw new InvalidOperationException($"Account {account.AccountNumber} already exists.");

            var dto = ToDTO(account);
            var task = _httpClient.PostAsJsonAsync(BaseUrl, dto);
            task.Wait();

            if (!task.Result.IsSuccessStatusCode)
                throw new Exception($"Error creating account in JSON Server. Status: {task.Result.StatusCode}");
        }

        public void Update(IBankAccount account)
        {
            var dto = ToDTO(account);
            var url = $"{BaseUrl}?accountNumber={dto.AccountNumber}";

            DeleteByAccountNumber(dto.AccountNumber); 
            var task = _httpClient.PostAsJsonAsync(BaseUrl, dto); 
            task.Wait();

            if (!task.Result.IsSuccessStatusCode)
                throw new Exception($"Error updating account in JSON Server. Status: {task.Result.StatusCode}");
        }

        private void DeleteByAccountNumber(string accountNumber)
        {
            var all = LoadAll();
            var remaining = all.Where(a => a.AccountNumber != accountNumber)
                               .Select(ToDTO)
                               .ToList();

            var deleteTask = _httpClient.DeleteAsync(BaseUrl);
            deleteTask.Wait();

            foreach (var dto in remaining)
            {
                var postTask = _httpClient.PostAsJsonAsync(BaseUrl, dto);
                postTask.Wait();
            }
        }

        public IEnumerable<IBankAccount> GetAll()
        {
            return LoadAll();
        }
    }
}
