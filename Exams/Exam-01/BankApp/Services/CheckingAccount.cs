namespace BankApp.Entities
{


 public class CheckingAccount : IBankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1000000;
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal OverdraftAmount { get; set; }
        public AccountType AccountType {get; set;}

        public CheckingAccount()
        {
            OverdraftAmount = MIN_OVERDRAFT_AMOUNT;
            BalanceAmount = OverdraftAmount;
            AccountType = AccountType.Checking;
        }

        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount - amount >= -OverdraftAmount)
                BalanceAmount -= amount;
            else
                Console.WriteLine("Excede el límite de sobregiro.");
        }
    }
    }