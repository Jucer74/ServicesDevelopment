using System.ComponentModel.DataAnnotations;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities
{
    public class Account : EntityBase
    {
        [Required]
        public char AccountType { get; set; } = 'A';
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public string AccountNumber { get; set; } = null!;
        [Required]
        public string OwnerName { get; set; } = null!;
        [Required]
        public decimal BalanceAmount { get; set; }
        [Required]
        public decimal OverdraftAmount { get; set; }
    }
}