using System;
using System.Collections.Generic;
using BankApp.Entities;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BankApp.Services
{
    public class BankService
    {
        private readonly List<IBankAccount> _accounts = new List<IBankAccount>();

        public void AddAccount(IBankAccount account)
        {
            if (_accounts.Exists(a => a.AccountNumber == account.AccountNumber))
            {
                Console.WriteLine($"Account {account.AccountNumber} already exists.");
                return;
            }

            _accounts.Add(account);
            Console.WriteLine($"Account {account.AccountNumber} created successfully.");
        }

        public IBankAccount GetAccount(string accountNumber)
        {
            return _accounts.Find(acc => acc.AccountNumber == accountNumber);
        }

        public void ShowAllBalances()
        {
            if (_accounts.Count == 0)
            {
                Console.WriteLine("No accounts registered.");
                return;
            }

            foreach (var account in _accounts)
            {
                account.ShowBalance();
            }
        }
    }
}
