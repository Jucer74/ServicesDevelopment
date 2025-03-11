namespace BankApp
{
    public enum AccountType
    {
        Saving = 1,   // Ahorros
        Checking = 2  // Corriente
    }

    public class BackAccount
    {
        private string AccountNumber { get; set; }
        private string AccountOwner { get; set; }
        private decimal BalanceAmount { get; set; }

        public AccountType AccountType { get; set; }

        public Account(string accountNumber, string accountOwner, decimal balanceAmount, AccountType accountType)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = balanceAmount;
            AccountType = accountType;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
                BalanceAmount += amount;
            else
                throw new ArgumentException("El monto a depositar debe ser mayor a cero.");
        }

        public void Withdrawal(decimal amount)
        {
            if (amount > 0)
            {
                if (BalanceAmount >= amount)
                    BalanceAmount -= amount;
                else
                    throw new InvalidOperationException("Fondos insuficientes para la transacción.");
            }
            else
            {
                throw new ArgumentException("El monto a retirar debe ser mayor a cero.");
            }
        }
    }
}
