using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Application.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[AC]$", ErrorMessage = "AccountType must be 'A' or 'C'")]
        public char AccountType { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string OwnerName { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "BalanceAmount must be greater than 0")]
        public decimal BalanceAmount { get; set; }

        public decimal OverdraftAmount { get; set; }
    }
}