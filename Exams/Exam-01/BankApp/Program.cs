// See https://aka.ms/new-console-template for more information

using BankApp.Services;
using BankApp.UI;

class Program
{
    static void Main()
    {
        BankService bankService = new BankService();
        Menu menu = new Menu(bankService);
        menu.ShowMenu();
    }
}
