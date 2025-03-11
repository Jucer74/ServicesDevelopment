using System;
using System.Threading.Tasks;
using BankApp.BL;
using BankApp.Entities;

namespace BankApp.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BankAccountService bankAccountService = new BankAccountService();

            while (true)
            {
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Check Balance");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string? option = Console.ReadLine();

                try
                {
                    if (option == "1")
                    {
                        // Código para crear cuenta
                    }
                    else if (option == "2")
                    {
                        // Código para depositar
                    }
                    else if (option == "3")
                    {
                        // Código para retirar
                    }
                    else if (option == "4")
                    {
                        // Código para ver saldo
                    }
                    else if (option == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}