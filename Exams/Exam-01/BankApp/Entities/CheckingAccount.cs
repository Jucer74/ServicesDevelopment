namespace BankApp.Entities
{
    // La clase CheckingAccount implementa IBankAccount para las cuentas corrientes y agrega la propiedad OverdraftAmount.
    public class CheckingAccount : IBankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1000000; // Valor mínimo de sobregiro.

        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Checking;

        // Propiedad específica de las cuentas corrientes para registrar el valor de sobregiro utilizado.
        public decimal OverdraftAmount { get; set; }

        public CheckingAccount() { }

        // Constructor para la creación de la cuenta. Se suma el sobregiro mínimo al saldo inicial.
        public CheckingAccount(string accountNumber, string accountOwner, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = initialBalance + MIN_OVERDRAFT_AMOUNT;
            OverdraftAmount = 0;
        }

        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
            // Si el balance alcanza o supera el mínimo, no se utiliza sobregiro.
            if (BalanceAmount >= MIN_OVERDRAFT_AMOUNT)
                OverdraftAmount = 0;
            else
                OverdraftAmount = MIN_OVERDRAFT_AMOUNT - BalanceAmount;
        }

        public void Withdrawal(decimal amount)
        {
            // Permitir el retiro siempre que el balance resultante sea > 0
            if (BalanceAmount - amount <= 0)
                throw new System.Exception("Insufficient funds including overdraft limit");

            BalanceAmount -= amount;

            // Si el nuevo balance es menor que MIN_OVERDRAFT_AMOUNT, se usa parte del sobregiro.
            if (BalanceAmount < MIN_OVERDRAFT_AMOUNT)
                OverdraftAmount = MIN_OVERDRAFT_AMOUNT - BalanceAmount;
            else
                OverdraftAmount = 0;
        }
    }
}
