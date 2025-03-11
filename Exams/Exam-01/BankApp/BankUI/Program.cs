using System;
using System.Threading.Tasks;
using Models;
using BankServices;
using System.ComponentModel.Design;

namespace Models
{
    class Program
    {
        static void Main()
        {
            Menu();
        }

        static void Menu()
        {
            while (true)
                {
                Console.WriteLine("\nBank System Menu:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit Amount");
                Console.WriteLine("3. Withdraw Amount");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    DepositAmount();
                    break;
                case "3":
                    WithdrawalAmount();
                    break;
                case "4":
                    GetBalance();
                    break;
                case "0":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
                }
            }

        }

        static void CreateAccount()
        {
            string accountNumber = GetValidAccountNumber();
            string accountOwner = GetValidAccountOwnwer();
        }

        static void DepositAmount()
        {

        }

        static void WithdrawalAmount()
        {

        }

        static void GetBalance()
        {

        }

        static bool IsValidAccountNumber(string accountNumber) 
        {
            return accountNumber.Length == 10 &&accountNumber.All(char.IsDigit);
        }

        static bool IsValidAmount(string inputAmount)
        {
            return true;
        }

        static bool IsValidAccountOwner(string accountNumber)
        {
            return true;
        }

        static int GetValidAccountType()
        {
            return 1;
        }

        static string GetValidAccountNumber()
        {
            string accountNumber;
            do
            {   

                Console.WriteLine("Enter account number (10-Digit Number): ");
                accountNumber = Console.ReadLine();
                   
                
            } while (!IsValidAccountNumber(accountNumber));
            return accountNumber;
        }

        static decimal GetValidAmount(string title)
        {
            return 1;
        }

        static string GetValidAccountOwnwer()
        {
            string accountOwner;
            do
            {
                Console.Write("Enter account owner: ");
                accountOwner = Console.ReadLine();
            } while (!IsValidAccountOwner(accountOwner));

            return accountOwner;
        }


    }
}