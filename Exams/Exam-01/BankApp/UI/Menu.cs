using System;
using BankApp.Entities;
using BankApp.Services;

namespace BankApp.UI
{
    public class Menu
    {
        private readonly BankService _bankService;

        public Menu(BankService bankService)
        {
            _bankService = bankService;
        }

        public void ShowMenu()
        {
            while (true)
            {
               Console.WriteLine("\n--- BANK MENU ---");
                Console.WriteLine("1 - Create Account");
                Console.WriteLine("2 - Check Balance");
                Console.WriteLine("3 - Deposit");
                Console.WriteLine("4 - Withdraw");
                Console.WriteLine("0 - Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        CheckBalance();
                        break;
                    case "3":
                        DepositMoney();
                        break;
                    case "4":
                        WithdrawMoney();
                        break;
                    case "0":
                        Console.WriteLine("Thank you for using the bank. See you soon!");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void CreateAccount()
        {
            Console.Write("Account number (10 digits): ");
            string accountNumber = Console.ReadLine();
            Console.Write("Owner's name: ");
            string owner = Console.ReadLine();
            Console.Write("Initial balance: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                Console.WriteLine("Invalid balance.");
                return;
            }

            Console.Write("Tipo de cuenta (1 - Ahorros, 2 - Corriente): ");
            IBankAccount newAccount = Console.ReadLine() == "2"
                ? new CheckingAccount(accountNumber, owner, initialBalance)
                : new SavingAccount(accountNumber, owner, initialBalance);

            _bankService.AddAccount(newAccount);
        }

        private void CheckBalance()
        {
            Console.Write("Account number: ");
            _bankService.GetAccount(Console.ReadLine())?.ShowBalance();
        }

        private void DepositMoney()
        {
            Console.Write("Account number: ");
            _bankService.GetAccount(Console.ReadLine())?.Deposit(100);
        }

        private void WithdrawMoney()
        {
            Console.Write("Account number: ");
            _bankService.GetAccount(Console.ReadLine())?.Withdraw(100);
        }
    }
}