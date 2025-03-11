using BankApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BankApp.Services
{
    // DTO para la serialización/deserialización con JSON Server
    public class BankAccountDTO
    {
        public string Type { get; set; }             // "Saving" o "Checking"
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal OverdraftAmount { get; set; } // Solo se usa si Type == "Checking"
    }

    public class BankAccountJsonRepository : IBankAccountRepository
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:3000/accounts";

        public BankAccountJsonRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Convierte un BankAccountDTO a IBankAccount (SavingAccount o CheckingAccount)
        private IBankAccount FromDTO(BankAccountDTO dto)
        {
            if (dto.Type == "Saving")
            {
                // Creamos un SavingAccount
                var saving = new SavingAccount(dto.AccountNumber, dto.AccountOwner, dto.BalanceAmount);
                return saving;
            }
            else if (dto.Type == "Checking")
            {
                // Creamos un CheckingAccount con "dto.BalanceAmount - 1_000_000"
                // porque en CheckingAccount el constructor suma el sobregiro mínimo
                // y en el JSON guardamos el "BalanceAmount" neto.
                var initialBalance = dto.BalanceAmount - 1_000_000m;
                if (initialBalance < 0) initialBalance = 0; // Evitar valores inconsistentes

                var checking = new CheckingAccount(dto.AccountNumber, dto.AccountOwner, initialBalance);
                // Forzamos OverdraftAmount si existía en DTO
                if (dto.OverdraftAmount > 0)
                {
                    // Ajustamos la diferencia
                    checking.Withdrawal(dto.OverdraftAmount);
                }
                return checking;
            }
            else
            {
                throw new ArgumentException($"Invalid type: {dto.Type}");
            }
        }

        // Convierte un IBankAccount (SavingAccount o CheckingAccount) a BankAccountDTO
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

        // Descarga todos los DTO desde el endpoint y los convierte a IBankAccount
        private IEnumerable<IBankAccount> LoadAll()
        {
            var task = _httpClient.GetFromJsonAsync<List<BankAccountDTO>>(BaseUrl);
            task.Wait(); // Simplificado, para no usar async/await
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
            return all.FirstOrDefault(a => a.AccountNumber == accountNumber);
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
            // Asumimos que la clave primaria es "AccountNumber" en JSON Server
            // Podríamos necesitar un ID, pero aquí forzamos a usar el accountNumber como ID
            var url = $"{BaseUrl}?accountNumber={dto.AccountNumber}";

            // OJO: json-server no filtra por "accountNumber" por defecto como ID
            // Para un PUT real, necesitaríamos un "id" en la entidad. 
            // Aquí hacemos un GET, eliminamos, creamos, o algo similar.
            // Ejemplo simplificado:
            DeleteByAccountNumber(dto.AccountNumber); // Eliminamos
            var task = _httpClient.PostAsJsonAsync(BaseUrl, dto); // Creamos de nuevo
            task.Wait();

            if (!task.Result.IsSuccessStatusCode)
                throw new Exception($"Error updating account in JSON Server. Status: {task.Result.StatusCode}");
        }

        // Función auxiliar para eliminar por accountNumber (json-server no lo hace nativamente)
        private void DeleteByAccountNumber(string accountNumber)
        {
            // Cargamos todas, filtramos, resubimos
            var all = LoadAll();
            var remaining = all.Where(a => a.AccountNumber != accountNumber)
                               .Select(ToDTO)
                               .ToList();

            // Borramos toda la colección y la volvemos a crear
            // (Es un truco muy simplificado para json-server)
            // En un entorno real, deberíamos usar un ID autoincremental
            // y endpoints como /accounts/1, /accounts/2, etc.
            var deleteTask = _httpClient.DeleteAsync(BaseUrl);
            deleteTask.Wait();

            // Vaciamos y volvemos a POSTear
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
