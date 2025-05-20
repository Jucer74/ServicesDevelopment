using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities
{
    [Table("Accounts")]
    public class Account : EntityBase
    {
       
        public char AccountType { get; set; } = 'A';

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string AccountNumber { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string OwnerName { get; set; } = null!;
        
        [Range(0.01, double.MaxValue)]
        public decimal BalanceAmount { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public decimal OverdraftAmount { get; set; } = 0;
    }
}