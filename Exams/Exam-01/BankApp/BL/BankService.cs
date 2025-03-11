using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class BankService
    {
        private List<BankAccount> _accounts = new List<BankAccount>();

        public BankAccount CreateAccount(BankAccount bankAccount)
        {
           
            return bankAccount;
        }

        public BankAccount GetBalanceAccount(string accountNumber)
        {
           
            return account;
        }

        public BankAccount DepositAmount(string accountNumber, decimal amountValue)
        {
            var account = GetBalanceAccount(accountNumber);
            account.Deposit(amountValue);
            return account;
        }

        public BankAccount WithdrawalAmount(string accountNumber, decimal amountValue)
        {
             
            return account;
        }
    }
}