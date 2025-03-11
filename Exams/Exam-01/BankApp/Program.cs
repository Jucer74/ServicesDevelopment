using System;
using System.Threading.Tasks;
using BankApp.UI;

namespace BankApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Menu menu = new Menu();
            await menu.ShowAsync();
        }
    }
}
