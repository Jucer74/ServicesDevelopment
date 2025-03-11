
using BankApp.entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankApp.bankService
{
    
    public class BankService
    {

        public static BankAccount GetAccount(string accountNumber)
        {
            using HttpClient client = new HttpClient();

            

            return new BankAccount("123456", "Juan Perez", 1000.50m, 1, 500m);
        }
        public static BankAccount CreateAccount(BankAccount bankAccount)
        {
            return bankAccount;
        }

        public static decimal GetBalanceAccount(string accountNumber)
        {
            // Get balance logic
            return 0;
        }

        public static void DepositAmount(string accountNumber, decimal amountValue)
        {
            // Deposit logic
        }

        public void Withdraw(decimal amount)
        {
            // Withdraw logic
        }

        public void Transfer(decimal amount, string accountNumber)
        {
            // Transfer logic
        }
    }

}