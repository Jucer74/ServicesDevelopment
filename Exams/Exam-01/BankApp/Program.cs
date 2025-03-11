using System;

namespace BankApp
{
    public class BankApp
    {
        public static void Main(string[] args)
        {
            BankApp app = new BankApp();
            app.Menu();
        }

        private readonly BankService bankService;

        public BankApp()
        {
            bankService = new BankService();
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Banking Operation");
                Console.WriteLine("-----------------");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Get Balance");
                Console.WriteLine("3. Deposit Amount");
                Console.WriteLine("4. Withdraw Amount");
                Console.WriteLine("0. Exit");
                Console.Write("Select Option: ");

                string input = Console.ReadLine() ?? "";
                if (!int.TryParse(input, out int option))
                {
                    Console.WriteLine("Invalid option. Press Enter to continue...");
                    Console.ReadLine();
                    continue;
                }

                switch (option)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        GetBalance();
                        break;
                    case 3:
                        DepositAmount();
                        break;
                    case 4:
                        WithdrawAmount();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void CreateAccount()
        {
            Console.Clear();
            Console.WriteLine("Create Account");
            Console.WriteLine("--------------");

            AccountType accountType = GetValidAccountType();
            string accountNumber = GetValidAccountNumber();
            string accountOwner = GetValidAccountOwner();
            decimal balanceAmount = GetValidAmount("Balance Amount");

            CreateBankAccount(accountNumber, accountOwner, balanceAmount, accountType);

            Console.WriteLine("Account created successfully\n\nPress any key to continue...");
            Console.ReadLine();
        }

        public void GetBalance()
        {
            Console.Clear();
            Console.WriteLine("Get Balance");
            Console.WriteLine("-----------");

            try
            {
                string accountNumber = GetValidExistingAccountNumber();
                var account = bankService.GetBalanceAccount(accountNumber).Result;

                Console.WriteLine($"\nAccount Number: {account.AccountNumber}");
                Console.WriteLine($"Account Type = {account.AccountType}");
                Console.WriteLine($"Placeholder = {account.AccountOwner}");
                Console.WriteLine($"Balance Amount = {account.BalanceAmount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        public void DepositAmount()
        {
            Console.Clear();
            Console.WriteLine("Deposit Amount");
            Console.WriteLine("--------------");

            try
            {
                string accountNumber = GetValidExistingAccountNumber();
                decimal amount = GetValidAmount("Deposit Amount");

                var account = bankService.DepositAmount(accountNumber, amount).Result;

                Console.WriteLine($"\nAccount Number: {account.AccountNumber}");
                Console.WriteLine($"Account Type = {account.AccountType}");
                Console.WriteLine($"Placeholder = {account.AccountOwner}");
                Console.WriteLine($"Balance Amount = {account.BalanceAmount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void WithdrawAmount()
        {
            Console.WriteLine("Withdraw feature is not implemented yet.");
        }

        private bool IsValidAccountNumber(string accountNumber)
        {
            return !string.IsNullOrEmpty(accountNumber) && accountNumber.Length == 10;
        }

        private bool IsValidAmount(string inputAmount)
        {
            return decimal.TryParse(inputAmount, out decimal amount) && amount > 0;
        }

        private bool IsValidAccountOwner(string accountOwner)
        {
            return !string.IsNullOrEmpty(accountOwner) && accountOwner.Length <= 50;
        }

        private AccountType GetValidAccountType()
        {
            while (true)
            {
                Console.Write("Account Type (1-Saving, 2-Checking): ");
                string input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out int accountType) && Enum.IsDefined(typeof(AccountType), accountType))
                {
                    return (AccountType)accountType;
                }
                Console.WriteLine("Invalid account type. Please enter 1 or 2.");
            }
        }

        private string GetValidAccountNumber()
        {
            while (true)
            {
                Console.Write("Account Number: ");
                string accountNumber = Console.ReadLine() ?? "";

                if (!IsValidAccountNumber(accountNumber))
                {
                    Console.WriteLine("Account Number is required and must have 10 digits");
                    continue;
                }

                return accountNumber;
            }
        }

        private string GetValidExistingAccountNumber()
        {
            while (true)
            {
                Console.Write("Account Number: ");
                string accountNumber = Console.ReadLine() ?? "";

                if (!IsValidAccountNumber(accountNumber))
                {
                    Console.WriteLine("Account Number is required and must have 10 digits");
                    continue;
                }

                var account = bankService.GetBalanceAccount(accountNumber).Result;
                if (account == null)
                {
                    Console.WriteLine($"Account: {accountNumber} doesn't exist.");
                    continue;
                }

                return accountNumber;
            }
        }

        private string GetValidAccountOwner()
        {
            while (true)
            {
                Console.Write("Account Owner: ");
                string accountOwner = Console.ReadLine() ?? "";

                if (!IsValidAccountOwner(accountOwner))
                {
                    Console.WriteLine("Account Owner is required and max length is 50 characters");
                    continue;
                }

                return accountOwner;
            }
        }

        private decimal GetValidAmount(string titleAmount)
        {
            while (true)
            {
                Console.Write($"{titleAmount}: ");
                string input = Console.ReadLine() ?? "";

                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                {
                    return amount;
                }

                Console.WriteLine("Amount is required and must be numeric and greater than zero.");
            }
        }
        private BankAccount CreateBankAccount(string accountNumber, string accountOwner, decimal initialBalanceAmount, AccountType accountType)
        {
            var newAccount = new BankAccount(accountNumber, accountOwner, initialBalanceAmount, accountType);
            bankService.CreateAccount(newAccount).Wait();
            return newAccount;
        }
    }
}
