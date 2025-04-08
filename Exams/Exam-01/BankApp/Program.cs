// See https://aka.ms/new-console-template for more information

using System;
using BankApp.Services;
using BankApp.UI;

namespace BankApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bankService = new BankService();
            var menu = new Menu(bankService);

            await menu.ShowMenuAsync(); // Llama al método asíncrono
        }
    }
}
