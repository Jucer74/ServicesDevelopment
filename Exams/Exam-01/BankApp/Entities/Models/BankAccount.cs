using BankApp.Entities.Enum;
using System;

using BankApp.Entities.Models;

namespace BankApp.Entities.Models
{


    public class BankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1000000;

        private string accountNumber;
        private string accountOwner;
        private decimal balanceAmount;
        private decimal overdraftAmount;

        public string AccountNumber
        {
            get => accountNumber;
            set
            {
                accountNumber = value;
            }
        }

        public string AccountOwner
        {
            get => accountOwner;
            set
            {
                accountOwner = value;
            }
        }

        public decimal BalanceAmount
        {
            get => balanceAmount;
             set
            {
                balanceAmount = value;
            }
        }

        public AccountType AccountType { get; set; }

        public decimal OverdraftAmount
        {
            get => overdraftAmount;
             set
            {
                overdraftAmount = value;
            }
        }

        public BankAccount(string accountNumber, string accountOwner, decimal initialBalance, AccountType accountType)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            AccountType = accountType;

            if (accountType == AccountType.Checking)
            {
                OverdraftAmount = 0;
                BalanceAmount = initialBalance + MIN_OVERDRAFT_AMOUNT;
            }
            else
            {
                OverdraftAmount = 0;
                BalanceAmount = initialBalance;
            }
        }

        public BankAccount(){

        }

        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
            CalculateOverdraft();
        }

        public void Withdrawal(decimal amount)
        {
            BalanceAmount -= amount;
            CalculateOverdraft();
        }

        public void CalculateOverdraft(){
            decimal overdraft = MIN_OVERDRAFT_AMOUNT - BalanceAmount;
            if (overdraft > 0){
                OverdraftAmount = overdraft;
            } else {
                OverdraftAmount = 0;
            }
        }
    }
}