using BankApp.Entities;

namespace BankApp.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
         public required string AccountNumber { get; set; }
        public required string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }
        public AccountType AccountType { get; init; }
        public decimal OverdraftAmount { get; set; }
        public decimal Balance { get; set; } = 0;
    }
}
