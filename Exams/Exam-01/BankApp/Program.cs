using System;
using BankApp.entities;
class Program
{
    static void Main()
    {
        Console.WriteLine("Bienvenido al Banco");



        BankAccount account = new BankAccount("123456", "Juan Perez", 1000.50m, 1,500m);

        Console.WriteLine($"Cuenta: {account.AccountNumber}");
    }
}
