namespace BankApp.Entities
{
    public interface IBankAccount
{
    string AccountNumber { get; set; }
    string AccountOwner { get; set; }
    decimal BalanceAmount { get; set; }
    
    void Deposit(decimal amount);
    bool Withdraw(decimal amount);
}

}
