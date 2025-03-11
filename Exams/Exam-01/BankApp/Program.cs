namespace BankApp
{
    public class BankApp
    {
        var BankService = new BankService();
        var BankAccount = new BankAccount();
        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Banking Operation");
                Console.WriteLine("-----------------");
                Console.WriteLine("1. Creaate Account");
                Console.WriteLine("2. Get Balance");
                Console.WriteLine("3. Deposit Amount");
                Console.WriteLine("4. Withdraw Amount");
                Console.WriteLine("0. Exit");
                Console.Write("Select Option: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int option))
                {
                    Console.WriteLine("Invalid option. Press Enter to continue...");
                    Console.ReadLine();
                    continue;
                }
             switch (input)
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

            while (true)
            {
                Console.Write("AccountTypepe (1-Saving , 2-Checking): ");
                int AccountTypepe = Console.ReadLine();
                if(AccountTypepe != 1 && AccountTypepe != 2)
                {
                    Console.WriteLine("Invalid AccountTypepe. Press Enter to continue...");
                    break;
                }
            }

            while(true)
            {
                Console.Write("Account Number: ");
                string accountOwner = Console.ReadLine();
                if(isValidAccountNumber(accountOwner) == false)
                {
                    Console.WriteLine("Account Number is required and must have 10 digits");");
                    break;
                }
            }

            while(true)
            {
                Console.Write("Account Owner: ");
                string accountOwner = Console.ReadLine();
                if(isValidAccountOwner(accountOwner) == false)
                {
                    Console.WriteLine("Account Owner is required and max length is 50 characters");
                    break;
                }
            }
            while(true)
            {
                Console.Write("Balance Amount: ");
                decimal balanceAmount = Console.ReadLine();
                if(isValidAmount(balanceAmount) == false)
                {
                    Console.WriteLine("Amount is required and must be numeric and greater than zero"); 
                    break;
                }
            }
            //AUN FALTA MANDAR LOS DATOS!
            newAccount = new BankAccount(accountNumber, accountOwner, balanceAmount, AccountType);
            BankService.CreateAccount(newAccount);
            Console.WriteLine("Account created successfully\n\nPress any key to continue...");
            Console.ReadLine();    
        }

        public void GetBalance()
        {
            Console.Clear();
            Console.WriteLine("Get Balance");
            Console.WriteLine("-----------");
            var response = GetValidAccountNumber();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        public void DepositAmount()
        {

        }

        public bool isValidAccountNumber(string accountNumber)
        {
            if(string.IsNullOrEmpty(accountNumber))
            {
                return false;
            }
            if(accountNumber.Length != 10)
            {
                return false;
            }
            return true;
        }

        public bool isValidAmount(string amount)
        {
            if(string.IsNullOrEmpty(amount))
            {
                return false;
            }
            if(amount <= 0)
            {
                return false;
            }
            return true;
        }

        public bool isValidAccountOwner(string accountOwner)
        {
            if(string.IsNullOrEmpty(accountOwner))
            {
                return false;
            }
            if(accountOwner.Length > 50)
            {
                return false;
            }
            return true;
        }

        public string GetValidAccountNumber()
        {
            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            var response BankService.GetBalanceAccount(accountNumber);
            return response;
        }
    }
}