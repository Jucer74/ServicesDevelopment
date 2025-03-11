using BankApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Services
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly List<IBankAccount> _accounts = new();

        public bool Exists(string accountNumber)
        {
            return _accounts.Any(a => a.AccountNumber == accountNumber);
        }

        public IBankAccount GetByAccountNumber(string accountNumber)
        {
            return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void Create(IBankAccount account)
        {
            if (Exists(account.AccountNumber))
                throw new InvalidOperationException($"Account {account.AccountNumber} already exists.");
            _accounts.Add(account);
        }

        public void Update(IBankAccount account)
        {
            // Para este ejemplo, en memoria no necesitamos hacer nada especial
            // porque los objetos se guardan por referencia.
            // Pero si fuera un JSON server, haríamos la llamada PUT/POST.
        }

        public IEnumerable<IBankAccount> GetAll()
        {
            return _accounts;
        }
    }
}
