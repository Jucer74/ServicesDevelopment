using System;
using System.Threading.Tasks;
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

        public async Task ShowMenuAsync()
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
                        await CreateAccountAsync();
                        break;
                    case "2":
                        await CheckBalanceAsync();
                        break;
                    case "3":
                        await DepositMoneyAsync();
                        break;
                    case "4":
                        await WithdrawMoneyAsync();
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

        private async Task CreateAccountAsync()
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

            await _bankService.AddAccountAsync(newAccount);
        }

        private async Task CheckBalanceAsync()
        {
            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine();
            var accounts = await _bankService.GetAccountsAsync();
            var account = accounts.Find(a => a.AccountNumber == accountNumber);
            account?.ShowBalance();
        }

        private async Task DepositMoneyAsync()
        {
            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                var accounts = await _bankService.GetAccountsAsync();
                var account = accounts.Find(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.Deposit(amount);
                    await _bankService.UpdateAccountAsync(account);
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        private async Task WithdrawMoneyAsync()
        {
            Console.Write("Account number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                var accounts = await _bankService.GetAccountsAsync();
                var account = accounts.Find(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.Withdraw(amount);
                    await _bankService.UpdateAccountAsync(account);
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
    }
}