namespace BankApp.Entities
{
    // Esta interfaz declara las propiedades y métodos que deben implementar todas las cuentas bancarias.
    public interface IBankAccount
    {
        string AccountNumber { get; set; }     // Número de cuenta: debe ser solo dígitos y 10 caracteres exactos.
        string AccountOwner { get; set; }      // Nombre del propietario: no vacío y máximo 50 caracteres.
        decimal BalanceAmount { get; set; }    // Saldo de la cuenta: mayor a cero.
        AccountType AccountType { get; set; }  // Tipo de cuenta: Saving o Checking.
        
        // Métodos para depositar y retirar dinero.
        void Deposit(decimal amount);
        void Withdrawal(decimal amount);
    }
}
