using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public string AccountNumber { get; set; } = null!;

        public decimal ValueAmount { get; set; }
    }
}
