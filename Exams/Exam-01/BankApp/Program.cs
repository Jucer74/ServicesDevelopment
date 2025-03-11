using System;
class Program
{
    static async Task Main()
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
                    Console.WriteLine("Getting balance...");
                    break;
                case "3":
                    Console.WriteLine("Depositing amount...");
                    break;
                case "4":
                    Console.WriteLine("Withdrawing amount...");
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

        Console.Write("Account Type (1-Saving, 2-Checking): ");
        string accountTypeInput = Console.ReadLine() ?? string.Empty;;
        string accountType = accountTypeInput == "1" ? "Saving" : "Checking";

        string accountNumber = ValidAccountMenu();

        Console.Write("Account Owner: ");
        string accountOwner = Console.ReadLine() ?? string.Empty;

        Console.Write("Balance Amount: ");
        decimal balanceAmount;
        while (!decimal.TryParse(Console.ReadLine(), out balanceAmount))
        {
            Console.Write("Invalid input. Enter a valid balance amount: ");
        }

        Console.Clear();
        Console.WriteLine("Account Created Successfully!");
        Console.WriteLine("----------------------------");
        Console.WriteLine($"Account Type  : {accountType}");
        Console.WriteLine($"Account Number: {accountNumber}");
        Console.WriteLine($"Account Owner : {accountOwner}");
        Console.WriteLine($"Balance Amount: {balanceAmount:C}");

    }

    static bool IsValidAccountNumber(string accountNumber)
    {
        return accountNumber.Length == 10 && long.TryParse(accountNumber, out _);
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
                Console.WriteLine("❌ Invalid account number. It must be exactly 10 digits.");
            }

        } while (!IsValidAccountNumber(accountNumber));

        return accountNumber;
    }
}
