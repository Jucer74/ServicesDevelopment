using MoneyBankService.Domain.Entities.Base;

namespace MoneyBankService.Domain.Entities
{
    public class Account : EntityBase
    {
        public char AccountType { get; set; } = 'A';
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string AccountNumber { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public decimal BalanceAmount { get; set; }
        public decimal OverdraftAmount { get; set; }
    }
}