namespace BankApp.Entities
{
    public class SavingAccount : IBankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType {get; set;}

        public SavingAccount(){
            AccountType = AccountType.Saving;
        }

        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount >= amount)
                BalanceAmount -= amount;
            else
                Console.WriteLine("Fondos insuficientes.");
        }
    }
}