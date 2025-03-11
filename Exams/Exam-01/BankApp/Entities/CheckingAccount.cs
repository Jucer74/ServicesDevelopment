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
        
        // Al crear una cuenta corriente, se suma el valor mínimo de sobregiro al saldo inicial.
        public CheckingAccount(string accountNumber, string accountOwner, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = initialBalance + MIN_OVERDRAFT_AMOUNT;
            OverdraftAmount = 0; // Inicialmente no se ha usado sobregiro.
        }
        
        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }
        
        // Permite retirar dinero siempre que, después del retiro, se respete el límite de sobregiro.
        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount - amount >= MIN_OVERDRAFT_AMOUNT)
                BalanceAmount -= amount;
            else
                throw new System.Exception("Insufficient funds including overdraft limit");
        }
    }
}
