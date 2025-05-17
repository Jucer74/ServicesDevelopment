using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Models
{
    [Table("accounts")]
    public class Account : EntityBase
    {
        [Required]
        public char AccountType { get; set; } = 'A';
        
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        
        [Required]
        [MaxLength(10)]
        public string AccountNumber { get; set; } = null!;
        
        [Required]
        [MaxLength(100)]
        public string OwnerName { get; set; } = null!;
        
        [Required]
        public decimal BalanceAmount { get; set; }
        
        [Required]
        public decimal OverdraftAmount { get; set; }
    }
}