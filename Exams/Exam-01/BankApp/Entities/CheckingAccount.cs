namespace BankApp.Entities
{
    public class CheckingAccount : BankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1000000;
        public decimal OverdraftAmount { get; private set; }

        public CheckingAccount(string accountNumber, string owner, decimal initialBalance)
            : base(accountNumber, owner, initialBalance + MIN_OVERDRAFT_AMOUNT)
        {
            OverdraftAmount = 0;
        }

        public override void Withdraw(decimal amount)
        {
    
            if (amount > Balance + OverdraftAmount)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            Balance -= amount;
            Console.WriteLine($"Withdrawal successful. New balance: {Balance:C}");
        }
    }
}