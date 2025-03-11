using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Entities;

namespace BankApp.Services
{
    public class BankService
    {
        private readonly DbService _dbService;

        public BankService()
        {
            _dbService = new DbService();
        }

        public async Task<bool> CreateAccountAsync(IBankAccount bankAccount)
        {
            var existingAccount = await _dbService.GetAccountByNumberAsync(bankAccount.AccountNumber);
            if (existingAccount != null)
            {
                Console.WriteLine("La cuenta ya existe.");
                return false;
            }

            return await _dbService.CreateAccountAsync(bankAccount);
        }

        public async Task<IBankAccount> GetBalanceAsync(string accountNumber)
        {
            var account = await _dbService.GetAccountByNumberAsync(accountNumber);
            if (account == null)
                Console.WriteLine("Cuenta no encontrada.");

            return account;
        }

        public async Task<bool> DepositAsync(string accountNumber, decimal amount)
        {
            var account = await _dbService.GetAccountByNumberAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return false;
            }

            account.BalanceAmount += amount;
            return await _dbService.UpdateAccountAsync(account);
        }

        public async Task<bool> WithdrawalAsync(string accountNumber, decimal amount)
        {
            var account = await _dbService.GetAccountByNumberAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return false;
            }

            if (account.AccountType == AccountType.Checking && account.BalanceAmount - amount < -((CheckingAccount)account).OverdraftAmount)
            {
                Console.WriteLine("Excede el límite de sobregiro.");
                return false;
            }

            if (account.BalanceAmount < amount)
            {
                Console.WriteLine("Fondos insuficientes.");
                return false;
            }

            account.BalanceAmount -= amount;
            return await _dbService.UpdateAccountAsync(account);
        }
    }
}
