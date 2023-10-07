using MoneyBankService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Domain.Entities
{
    public class Account : EntityBase
    {
        [Required(ErrorMessage = "The AccountType is required.")]
        [RegularExpression("^[AC]$", ErrorMessage = "The AccountType must be 'A' or 'C'.")]
        [StringLength(1, ErrorMessage = "The maximum length of AccountType is 1 character.")]
        public char AccountType { get; set; } = 'A';

        [DataType(DataType.Date, ErrorMessage = "The CreationDate must be a valid date.")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "The AccountNumber is required.")]
        [MaxLength(10, ErrorMessage = "The maximum length of AccountNumber is 10 characters.")]
        [RegularExpression(@"\d{10}", ErrorMessage = "The AccountNumber only accepts numbers.")]
        public string AccountNumber { get; set; } = null!;
        [Required(ErrorMessage = "The OwnerName is required.")]
        [MaxLength(100, ErrorMessage = "The maximum length of OwnerName is 100 characters.")]
        public string OwnerName { get; set; } = null!;
        [Required(ErrorMessage = "The BalanceAmount is required.")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "The BalanceAmount should be in money format (0.00)")]
        public decimal BalanceAmount { get; set; }
        [Required(ErrorMessage = "The OverdraftAmount is required.")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "The OverdraftAmount should be in money format (0.00)")]
        public decimal OverdraftAmount { get; set; }
    }
}