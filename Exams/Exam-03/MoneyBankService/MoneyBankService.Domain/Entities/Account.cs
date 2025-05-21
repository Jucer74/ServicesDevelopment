using System.ComponentModel.DataAnnotations;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities;

public class Account : EntityBase
{
    [Key]
    public new int Id { get; set; }

    [Required]
    [RegularExpression("[AC]")]
    public char AccountType { get; set; } = 'A';

    [DataType(DataType.Date)]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(10)]
    [RegularExpression(@"^\d{10}$")]
    public string AccountNumber { get; set; } = null!;

    [Required]
    public string OwnerName { get; set; } = null!;

    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public decimal BalanceAmount { get; set; }

    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public decimal OverdraftAmount { get; set; }
}
