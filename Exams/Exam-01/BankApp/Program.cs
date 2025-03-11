// See https://aka.ms/new-console-template for more information
Console.WriteLine("hello word examn");
namespace BankApp
{
    public static class Program
    {
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
                            GetUsers(userService);
                            break;
                        case 3:
                            UpdateUser(userService);
                            break;
                        case 4:
                            DeleteUser(userService);
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Por favor, ingrese un número.");
                }
            }
        }

        public static void CreateAccount()
        {
            BankAccount newBankAccount = new BankAccount();
            System.Console.WriteLine("AccounType (1-Saving , 2-Checking): ");
            int.TryParse(Console.ReadLine(), out int accountType);
            System.Console.WriteLine("Account Number: ");
            int.TryParse(Console.ReadLine(), out int accountNumber);
            System.Console.WriteLine("Account Owner: ");
            int.TryParse(Console.ReadLine(), out int accountOwner);
            System.Console.WriteLine("Balance Amount: ");
            int.TryParse(Console.ReadLine(), out int balanceAmount);

            if (accountType == 2) {

            }

            
            newBankAccount.accountType= numberType;
            //ValidateAccountType();
            // System.Console.WriteLine("Account Number:");
            // while (!IsValidAccountNumber(accountNumber)){
            //     System.Console.WriteLine("Account number must have 10 digits");
            // }



        }
        private bool ValidateAccountType()
        {
            System.Console.WriteLine("AccounType (1-Saving , 2-Checking): ");
            int.TryParse(Console.ReadLine(), out int option)
            if (option > 2 || option < 1)
            {
                System.Console.WriteLine("Por favor digite una opcion valida");
                ValidateAccountType();
            }
            return true;
        }

        public static void DepositAmount()
        {

        }

        public static void GetBalance()
        {

        }

        public static void WithdrawalAmount()
        {

        }

        public static bool IsValidAccountNumber(string accountNumber)
        {
            return false;
        }

        public static bool IsValidAmount(string inputAmount)
        {
            return false;
        }

        public static bool IsValidAccountOwner(string accountOwner)
        {
            return false;
        }

        public static AccountType GetValidAccountType()
        {

            return new AccountType();
        }

        public static string GetValidAccountNumber()
        {
            //verificar si existe en el json
            return "";
        }

        public static decimal GetValidAmount(string titleAmount)
        {
            return 0;
        }

        public static string GetValidAccountOwner()
        {
            return "";
        }

        public static BankAccount CreateBankAccount(AccountType accountType, string accountNumber, string accountOwner, decimal initialBalanceAmount)
        {
            return new BankAccount();
        }
        static async Task Main(string[] args)
        {

        }
    }
}