using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;

class Program 
{
    static BankService bankService = new BankService();

    static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("\n Menú Bancario");
            Console.WriteLine("1️. Crear cuenta");
            Console.WriteLine("2️. Consultar saldo");
            Console.WriteLine("3️. Depositar");
            Console.WriteLine("4️. Retirar");
            Console.WriteLine("0️. Salir");
            Console.Write("Seleccione una opción: ");

            string option = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (option)
            {
                case "1":
                    await CreateAccount();
                    break;
                case "2":
                    await GetBalance();
                    break;
                case "3":
                    await Deposit();
                    break;
                case "4":
                    await Withdraw();
                    break;
                case "0":
                    Console.WriteLine("Saliendo...");
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }

    static async Task CreateAccount()
    {
        Console.Write("Ingrese número de cuenta (10 dígitos): ");
        string accountNumber = Console.ReadLine()?.Trim();

        if (accountNumber == null || accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
        {
            Console.WriteLine("Número de cuenta inválido. Debe tener 10 dígitos.");
            return;
        }

        Console.Write("Ingrese el nombre del titular (máx. 50 caracteres): ");
        string accountOwner = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(accountOwner) || accountOwner.Length > 50)
        {
            Console.WriteLine("Nombre inválido.");
            return;
        }

        Console.Write("Ingrese el tipo de cuenta (1 = Ahorros, 2 = Corriente): ");
        if (!int.TryParse(Console.ReadLine(), out int accountTypeInput) ||
            !Enum.IsDefined(typeof(AccountType), accountTypeInput))
        {
            Console.WriteLine("Tipo de cuenta inválido.");
            return;
        }
        AccountType accountType = (AccountType)accountTypeInput;

        Console.Write("Ingrese el saldo inicial: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance) || initialBalance < 0)
        {
            Console.WriteLine("Saldo inicial inválido.");
            return;
        }

        IBankAccount account = accountType == AccountType.Saving
            ? new SavingAccount(accountNumber, accountOwner, initialBalance)
            : new CheckingAccount(accountNumber, accountOwner, initialBalance);

        await bankService.CreateAccount(account);
    }

    static async Task GetBalance()
    {
        Console.Write("Ingrese número de cuenta: ");
        string accountNumber = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            Console.WriteLine("Número de cuenta inválido.");
            return;
        }

        IBankAccount account = await bankService.GetBalance(accountNumber);
        if (account != null)
        {
            Console.WriteLine($"Saldo actual de {account.AccountOwner}: {account.BalanceAmount:C}");
        }
    }

    static async Task Deposit()
    {
        Console.Write("Ingrese número de cuenta: ");
        string accountNumber = Console.ReadLine()?.Trim();

        Console.Write("Ingrese monto a depositar: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Monto inválido.");
            return;
        }

        await bankService.DepositAccount(accountNumber, amount);
    }

    static async Task Withdraw()
    {
        Console.Write("Ingrese número de cuenta: ");
        string accountNumber = Console.ReadLine()?.Trim();

        Console.Write("Ingrese monto a retirar: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Monto inválido.");
            return;
        }

        await bankService.WithdrawalAccount(accountNumber, amount);
    }
}

public enum AccountType
{
    Saving = 1,
    Checking = 2
}

public interface IBankAccount
{
    string AccountNumber { get; }
    string AccountOwner { get; }
    decimal BalanceAmount { get; set; }
    AccountType AccountType { get; }
    void Deposit(decimal amount);
    void Withdrawal(decimal amount);
}

public class SavingAccount : IBankAccount
{
    public string AccountNumber { get; }
    public string AccountOwner { get; }
    public decimal BalanceAmount { get; set; }
    public AccountType AccountType => AccountType.Saving;

    public SavingAccount(string accountNumber, string accountOwner, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        BalanceAmount = initialBalance;
    }

    public void Deposit(decimal amount) => BalanceAmount += amount;

    public void Withdrawal(decimal amount)
    {
        if (BalanceAmount >= amount)
            BalanceAmount -= amount;
        else
            Console.WriteLine("Fondos insuficientes.");
    }
}

public class CheckingAccount : IBankAccount
{
    public string AccountNumber { get; }
    public string AccountOwner { get; }
    public decimal BalanceAmount { get; set; }
    public AccountType AccountType => AccountType.Checking;

    public CheckingAccount(string accountNumber, string accountOwner, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        BalanceAmount = initialBalance;
    }

    public void Deposit(decimal amount) => BalanceAmount += amount;

    public void Withdrawal(decimal amount)
    {
        if (BalanceAmount >= amount)
            BalanceAmount -= amount;
        else
            Console.WriteLine("Fondos insuficientes para sobregiro.");
    }
}

public class BankService
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiUrl = "http://localhost:3000/";

    public async Task CreateAccount(IBankAccount account)
    {
        var accountData = new
        {
            account.AccountNumber,
            account.AccountOwner,
            account.BalanceAmount,
            accountType = account.AccountType.ToString()
        };

        string json = JsonSerializer.Serialize(accountData);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(apiUrl, content);
        Console.WriteLine(response.IsSuccessStatusCode ? "Cuenta creada exitosamente." : "Error al crear cuenta.");
    }

    public async Task<IBankAccount> GetBalance(string accountNumber)
    {
        HttpResponseMessage response = await client.GetAsync($"{apiUrl}{accountNumber}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Cuenta no encontrada.");
            return null;
        }

        string json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            Console.WriteLine("Error al obtener los datos.");
            return null;
        }

        return JsonSerializer.Deserialize<SavingAccount>(json);
    }

    public async Task DepositAccount(string accountNumber, decimal amount) =>
        await SendTransaction(accountNumber, "deposit", amount);

    public async Task WithdrawalAccount(string accountNumber, decimal amount) =>
        await SendTransaction(accountNumber, "withdraw", amount);

    private async Task SendTransaction(string accountNumber, string type, decimal amount)
    {
        var payload = new { amount };
        string json = JsonSerializer.Serialize(payload);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PutAsync($"{apiUrl}{accountNumber}/{type}", content);
        Console.WriteLine(response.IsSuccessStatusCode ? "Operación exitosa." : "Error en la operación.");
    }
}
