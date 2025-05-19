using MoneyBankService.Domain.Entities.Base;

namespace MoneyBankService.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public string AccountNumber { get; set; } = null!;
        public decimal ValueAmount { get; set; }
    }
}