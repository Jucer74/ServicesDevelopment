namespace BankApp.Entities
{
    // La clase SavingAccount implementa la interfaz IBankAccount para las cuentas de ahorro.
    public class SavingAccount : IBankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Saving; // Se fija el tipo de cuenta a Saving.
        
        // Constructor vacío (por defecto).
        public SavingAccount() { }
        
        // Constructor con parámetros para facilitar la creación de cuentas.
        public SavingAccount(string accountNumber, string accountOwner, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = initialBalance;
        }
        
        // Incrementa el saldo.
        public void Deposit(decimal amount)
        {
            BalanceAmount += amount;
        }
        
        // Retira dinero, verificando que haya fondos suficientes.
        public void Withdrawal(decimal amount)
        {
            if (BalanceAmount >= amount)
                BalanceAmount -= amount;
            else
                throw new System.Exception("Insufficient funds");
        }
    }
}
