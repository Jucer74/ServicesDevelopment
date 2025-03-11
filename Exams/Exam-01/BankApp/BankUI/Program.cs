using System;
using System.Threading.Tasks;
using Models;
using BankServices;

namespace Models
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BankService bankService = new BankService();

            // string testAccountNumber = "1234567890";
            // decimal initialBalance = 1000m;
            // Console.WriteLine($"Testing with Account: {testAccountNumber}");

            // Console.WriteLine("Getting initial balance...");
            // Console.WriteLine($"Balance: {initialBalance}");

            // Console.WriteLine("Depositing 500...");
            // decimal depositAmount = 500m;
            // initialBalance += depositAmount;
            // Console.WriteLine($"New Balance: {initialBalance}");
            Console.WriteLine("Hello");

            Console.WriteLine("Fetching Users");
            var account = await bankService.GetAccount("1111111111");
             Console.WriteLine($"ID: {account.AccountNumber}, Name: {account.AccountOwner}, Balance: {account.BalanceAmount}");
           
            

            BankAccount newBank = new BankAccount("1334666111890", "Pepepga", 100000, 0, 0);

            Console.WriteLine("Enter User");
            await bankService.CreateAccount(newBank);

            Console.WriteLine("Get Balance");
            Console.WriteLine(await bankService.GetBalance("1111111111"));
            
            Console.WriteLine("Deposit Ammount");
            Console.WriteLine(await bankService.DepositAmount("1111111111", 100000));

            Console.WriteLine("Witdraw");
            Console.WriteLine(await bankService.WithdrawalAccount("1111111111", 1));
  
        }
    }
}