// See https://aka.ms/new-console-template for more information

using System.Threading.Tasks;

namespace BankApp
{
    class Program
    {
        static BankService BankService = new BankService();
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("\n       Banking Operation    ");
                Console.WriteLine("\n----------------------------");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Get Balance");
                Console.WriteLine("3. Deposit Amount");
                Console.WriteLine("4. Withdrawal Amount");
                Console.WriteLine("0. Exit");
                Console.Write("Select Option: ");

                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    if (option == 0)
                        break;

                    switch (option)
                    {
                        case 1:
                            CreateAccount();
                            break;
                        case 2:
                            System.Console.WriteLine("\nGet Balance Account");
                            System.Console.WriteLine("-------------------");
                            GetBalance().Wait();
                            break;
                        case 3:
                            System.Console.WriteLine("\nDeposit Account");
                            System.Console.WriteLine("---------------");
                            DepositAmount().Wait();
                            break;
                        case 4:
                            System.Console.WriteLine("Withdrawal Account");
                            System.Console.WriteLine("-------------------");
                            WithdrawalAmount().Wait();
                            break;
                        default:
                            Console.WriteLine("Option no valid");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Input no valid. Please, enter a number");
                }
            }
        }

        public static async Task CreateAccount()
        {
            BankAccount newBankAccount = new BankAccount();
            System.Console.WriteLine("AccounType (1-Saving , 2-Checking): ");
            int.TryParse(Console.ReadLine(), out int accountType);
            System.Console.WriteLine("Account Number: ");
            var accountNumber = Console.ReadLine();
            System.Console.WriteLine("Account Owner: ");
            string accountOwner = Console.ReadLine();
            System.Console.WriteLine("Balance Amount: ");
            int.TryParse(Console.ReadLine(), out int balanceAmount);
            int balanceAmountUpdate = AllocateCheckingsAccountAmount(accountType, balanceAmount);

            int newId = await BankService.GetLastAccountId();
            newBankAccount.Id = newId;
            newBankAccount.AccountType = (AccountType)accountType;
            newBankAccount.AccountNumber = accountNumber;
            newBankAccount.AccountOwner = accountOwner;
            newBankAccount.BalanceAmount = balanceAmountUpdate;
            newBankAccount.OverdraftAmount = 0;

            await BankService.CreateAccount(newBankAccount);

            System.Console.WriteLine("\nAccount created...");
        }
        private static int AllocateCheckingsAccountAmount(int accountType, int balanceAmount)
        {
            if (accountType == 2)
                return balanceAmount + 1000000;
            else
                return balanceAmount + 0;
        }

        public static async Task DepositAmount()
        {
            System.Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            System.Console.Write("Amount : ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);

            BankAccount bankAccount = await BankService.DepositAmount(accountNumber, amount);
            System.Console.WriteLine("Deposit Success");
        }

        public static async Task GetBalance()
        {
            System.Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            var bankAccount = await BankService.GetBalanceAccount(accountNumber);
            if (bankAccount != null)
            {
                System.Console.WriteLine("\nAccount Type = " + bankAccount.AccountType.ToString());
                System.Console.WriteLine("Placeholder = " + bankAccount.AccountOwner);
                System.Console.WriteLine("Balance Amount = " + bankAccount.BalanceAmount);
            }
        }

        public static async Task WithdrawalAmount()
        {
            System.Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            System.Console.Write("Amount : ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Must be a number greater than 0.");
                return;
            }
            BankAccount bankAccount = await BankService.WithdrawalAmount(accountNumber, amount);
            System.Console.WriteLine("Withdrawal Success ");
        }

        static async Task Main(string[] args)
        {
            Menu();
        }
    }
}