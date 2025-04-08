namespace BankApp.Entities
{
    public class SavingAccount : BankAccount
    {
        public SavingAccount(string accountNumber, string owner, decimal initialBalance)
            : base(accountNumber, owner, initialBalance) {}

        public override void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            Balance -= amount;
            Console.WriteLine($"Withdrawal successful. New balance: {Balance:C}");
        }
    }
}
