namespace BankApp.Entities
{
    public class BankAccount
    {
        public required string AccountNumber { get; set; }
        public required string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; set; }
        public decimal OverdraftAmount { get; set; }
    }
}