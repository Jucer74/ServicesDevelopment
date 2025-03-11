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
            Console.WriteLine("1️⃣ Crear Cuenta");
            Console.WriteLine("2️⃣ Consultar Saldo");
            Console.WriteLine("3️⃣ Depositar");
            Console.WriteLine("4️⃣ Retirar");
            Console.WriteLine("0️⃣ Salir");
            Console.Write("Seleccione una opción: ");

            string option = Console.ReadLine();
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
                    Console.WriteLine(" Saliendo...");
                    return;
                default:
                    Console.WriteLine(" Opción inválida.");
                    break;
            }
        }
    }

    static async Task CreateAccount()
    {
        Console.Write("Ingrese número de cuenta (10 dígitos): ");
        string accountNumber = Console.ReadLine();

        if (accountNumber.Length != 10 || !long.TryParse(accountNumber, out _))
        {
            Console.WriteLine("❌ Número de cuenta inválido.");
            return;
        }

        Console.Write("Ingrese el nombre del titular (máx. 50 caracteres): ");
        string accountOwner = Console.ReadLine();

        if (accountOwner.Length > 50 || string.IsNullOrWhiteSpace(accountOwner))
        {
            Console.WriteLine(" Nombre inválido.");
            return;
        }

        Console.Write("Ingrese el tipo de cuenta (1 = Ahorros, 2 = Corriente): ");
        if (!Enum.TryParse(Console.ReadLine(), out AccountType accountType))
        {
            Console.WriteLine("❌ Tipo de cuenta inválido.");
            return;
        }

        Console.Write("Ingrese el saldo inicial: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance) || initialBalance < 0)
        {
            Console.WriteLine(" Saldo inicial inválido.");
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
        string accountNumber = Console.ReadLine();

        IBankAccount account = await bankService.GetBalance(accountNumber);
        if (account != null)
        {
            Console.WriteLine($" Saldo actual de {account.AccountOwner}: {account.BalanceAmount:C}");
        }
    }

    static async Task Deposit()
    {
        Console.Write("Ingrese número de cuenta: ");
        string accountNumber = Console.ReadLine();

        Console.Write("Ingrese monto a depositar: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("❌ Monto inválido.");
            return;
        }

        await bankService.DepositAccount(accountNumber, amount);
    }

    static async Task Withdraw()
    {
        Console.Write("Ingrese número de cuenta: ");
        string accountNumber = Console.ReadLine();

        Console.Write("Ingrese monto a retirar: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("❌ Monto inválido.");
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
        if (BalanceAmount >= amount) BalanceAmount -= amount;
        else Console.WriteLine(" Fondos insuficientes.");
    }
}

public class CheckingAccount : IBankAccount
{
    private const decimal MIN_OVERDRAFT_AMOUNT = 1000000;
    public string AccountNumber { get; }
    public string AccountOwner { get; }
    public decimal BalanceAmount { get; set; }
    public AccountType AccountType => AccountType.Checking;
    public decimal OverdraftAmount { get; }

    public CheckingAccount(string accountNumber, string accountOwner, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        BalanceAmount = initialBalance + MIN_OVERDRAFT_AMOUNT;
        OverdraftAmount = MIN_OVERDRAFT_AMOUNT;
    }

    public void Deposit(decimal amount) => BalanceAmount += amount;
    public void Withdrawal(decimal amount)
    {
        if (BalanceAmount - amount >= -OverdraftAmount) BalanceAmount -= amount;
        else Console.WriteLine(" Fondos insuficientes para sobregiro.");
    }
}

public class BankService
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiUrl = "http://localhost:3000/";

    public async Task CreateAccount(IBankAccount account)
    {
        string json = JsonSerializer.Serialize(account);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{apiUrl}accounts", content);
        Console.WriteLine(response.IsSuccessStatusCode ? " Cuenta creada exitosamente." : "❌ Error al crear cuenta.");
    }

    public async Task<IBankAccount> GetBalance(string accountNumber)
    {
        HttpResponseMessage response = await client.GetAsync($"{apiUrl}accounts/{accountNumber}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(" Cuenta no encontrada.");
            return null;
        }

        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CheckingAccount>(json);
    }

    public async Task DepositAccount(string accountNumber, decimal amount)
    {
        var payload = new { amount };
        string json = JsonSerializer.Serialize(payload);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PutAsync($"{apiUrl}accounts/{accountNumber}/deposit", content);
        Console.WriteLine(response.IsSuccessStatusCode ? " Depósito exitoso." : "❌ Error al depositar.");
    }

    public async Task WithdrawalAccount(string accountNumber, decimal amount)
    {
        var payload = new { amount };
        string json = JsonSerializer.Serialize(payload);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PutAsync($"{apiUrl}accounts/{accountNumber}/withdraw", content);
        Console.WriteLine(response.IsSuccessStatusCode ? " Retiro exitoso." : " Fondos insuficientes o cuenta no encontrada.");
    }
}
