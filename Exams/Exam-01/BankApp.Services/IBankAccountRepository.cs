using BankApp.Entities;
using System.Collections.Generic;

namespace BankApp.Services
{
    public interface IBankAccountRepository
    {
        bool Exists(string accountNumber);
        IBankAccount GetByAccountNumber(string accountNumber);
        void Create(IBankAccount account);
        void Update(IBankAccount account);
        IEnumerable<IBankAccount> GetAll();
    }
}
