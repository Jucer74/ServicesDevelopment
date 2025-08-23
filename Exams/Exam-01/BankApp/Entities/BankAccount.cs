namespace Entities
{
    public class BankAccount
    {
        public string? Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public int AccountType { get; set; }
        public decimal OverdraftAmount { get; set; }
    }
}