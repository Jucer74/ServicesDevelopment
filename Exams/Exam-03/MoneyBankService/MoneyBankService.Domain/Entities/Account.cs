using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
