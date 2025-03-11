namespace Models {
      public interface IBankAccount
    {
        string AccountNumber { get; }
        string AccountOwner { get; }
        decimal BalanceAmount { get; }
        int AccountType {get; }


    }
    public class BankAccount : IBankAccount
    {
    public string AccountNumber { get; private set; }
    public string AccountOwner { get; private set; }
    public decimal BalanceAmount { get; private set; }
    public int AccountType { get; private set; }
    }
}

