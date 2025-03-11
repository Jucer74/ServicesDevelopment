using BankApp.entities;
using BankApp.bankService;
using System.Threading.Tasks;
class Program
{
    static  void Main()
    {

        Menu();

    }

    static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("    Banking Operation    ");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Get Balance");
            Console.WriteLine("3. Deposit Amount");
            Console.WriteLine("4. Withdrawal Amount");
            Console.WriteLine("0. Exit");
            Console.Write("Select Option: ");

            string input = Console.ReadLine() ?? string.Empty;

            switch (input)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    GetBalance();
                    break;
                case "3":
                    DepositAmount();
                    break;
                case "4":
                    WithdrawAmount();
                    break;
                case "0":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public static void CreateAccount()
    {
        Console.Clear();
        Console.WriteLine("Create Account");
        Console.WriteLine("--------------");

        int accountType = ValidAccountTypeMenu();

        string accountNumber = ValidAccountMenu();

        string accountOwner = ValidAccountOwnerMenu();

        decimal balanceAmount = ValidAccountBalanceMenu();

        BankAccount newAccount = CreateBankAccount(accountType, accountNumber, accountOwner, balanceAmount);

        createdAccountMenu(newAccount);

    }

    public static async void GetBalance()
    {
        Console.Clear();
        Console.WriteLine("Get Balance");
        Console.WriteLine("--------------");

        string accountNumber = ValidAccountMenu();

        try
        {
            BankAccount account =  await BankService.GetAccount(accountNumber);

            if (account.AccountType == 1)
            {
                
                Console.WriteLine($"Account Type  : {account.AccountType}");
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Account Owner : {account.AccountOwner}");
                Console.WriteLine($"Balance Amount: {account.BalanceAmount:C}");
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Account Type  : {account.AccountType}");
                Console.WriteLine($"Account Number: {account.AccountNumber + account.OverdraftAmount}");
                Console.WriteLine($"Account Owner : {account.AccountOwner}");
                Console.WriteLine($"Balance Amount: {account.BalanceAmount:C}");
                Console.WriteLine($"Overdraft Amount: {1000000 - account.OverdraftAmount}");
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
        catch (Exception error)
        {
            Console.WriteLine($"Se produjo un error {error.Message}");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


    }

    public static async void DepositAmount()
    {

        Console.Clear();
        Console.WriteLine("Deposit Amount");
        Console.WriteLine("--------------");

        string accountNumber = ValidAccountMenu();

        decimal balanceAmount = ValidAccountBalanceMenu();

        try
        {
            BankAccount account =  await BankService.DepositAmount(accountNumber, balanceAmount);

                
                Console.WriteLine("Deposit Success");
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
        }
        catch (Exception error)
        {
            Console.WriteLine($"Se produjo un error {error.Message}");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }

    public static async void WithdrawAmount()
    {

        Console.Clear();
        Console.WriteLine("Withdrawal Amount");
        Console.WriteLine("--------------");

        string accountNumber = ValidAccountMenu();

        decimal withdrawAmout = ValidAccountBalanceMenu();

        try
        {
            BankAccount account =  await BankService.Withdraw(accountNumber, withdrawAmout);

                
                Console.WriteLine("Withdrawal Success");
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
        }
        catch (Exception error)
        {
            Console.WriteLine($"Se produjo un error {error.Message}");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }



    static bool IsValidAccountNumber(string accountNumber)
    {
        return accountNumber.Length == 10 && long.TryParse(accountNumber, out _);
    }

    static bool IsValidTypeAccount(string input, out int option)
    {
        return int.TryParse(input, out option) && (option == 1 || option == 2);
    }

    static bool IsValidAccountOwner(string input)
    {

        bool isNotNullOrEmpty = !string.IsNullOrEmpty(input);
        bool isLessThan50Chars = input.Length <= 50;

        return isNotNullOrEmpty && isLessThan50Chars;
    }

    static bool IsValidAccountBalance(string input, out decimal balance)
    {
        return decimal.TryParse(input, out balance);
    }

    static int ValidAccountTypeMenu()
    {
        int option;
        string input;

        do
        {
            Console.Write("Select an option (1 - Saving, 2 - Checking): ");
            input = Console.ReadLine() ?? string.Empty;

            if (!IsValidTypeAccount(input, out option))
            {
                Console.WriteLine("❌ Invalid option. Please enter 1 or 2.");
            }

        } while (!IsValidTypeAccount(input, out option));

        return option;
    }
    static string ValidAccountMenu()
    {
        string accountNumber;

        do
        {
            Console.Write("Account Number: ");
            accountNumber = Console.ReadLine() ?? string.Empty;

            if (!IsValidAccountNumber(accountNumber))
            {
                Console.WriteLine("Invalid account number. It must be exactly 10 digits.");
            }

        } while (!IsValidAccountNumber(accountNumber));

        return accountNumber;
    }

    static string ValidAccountOwnerMenu()
    {
        string input;

        do
        {
            Console.Write("Account Owner (max 50 characters, not empty): ");
            input = Console.ReadLine() ?? string.Empty;

            if (!IsValidAccountOwner(input))
            {
                Console.WriteLine("❌ The input cannot be empty or more than 50 characters.");
            }


        } while (!IsValidAccountOwner(input));

        return input;
    }

    static decimal ValidAccountBalanceMenu()
    {
        decimal balance;
        string input;

        do
        {
            Console.Write("Balance Amount: ");
            input = Console.ReadLine() ?? string.Empty;

            if (!IsValidAccountBalance(input, out balance))
            {
                Console.WriteLine("❌ Invalid option. Please enter a decimal");
            }

        } while (!IsValidAccountBalance(input, out balance));

        return balance;
    }


    static BankAccount CreateBankAccount(int accountType, string accountNumber, string accountOwner, decimal balanceAmount)
    {

        decimal accountOverdraft;

        if (accountType == 1)
        {
            accountOverdraft = 0;
        }
        else {
            accountOverdraft = 1000000;
        }

        BankAccount newAccount = new BankAccount(accountNumber, accountOwner, balanceAmount, accountType, accountOverdraft);

        return newAccount;

    }

    static async void createdAccountMenu(BankAccount newAccount)
    {
        try
        {
            BankAccount accountPosted =  await BankService.CreateAccount(newAccount);

            Console.Clear();
            Console.WriteLine("Account Created Successfully!");
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Account Type  : {accountPosted.AccountType}");
            Console.WriteLine($"Account Number: {accountPosted.AccountNumber}");
            Console.WriteLine($"Account Owner : {accountPosted.AccountOwner}");
            Console.WriteLine($"Balance Amount: {accountPosted.BalanceAmount:C}");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        catch (Exception error)
        {
            Console.WriteLine($"Se produjo un error {error.Message}");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }

}
