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

        public string AccountNumber { get; set; } = null!;

        public string OwnerName { get; set; } = null!;
        public decimal BalanceAmount { get; set; } = 0;

        public decimal OverdraftAmount { get; set; } = 0;
    }
}