using System.ComponentModel.DataAnnotations;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Entities;

public class Transaction : EntityBase
{
    [Required]
    public string AccountNumber { get; set; } = null!;
    [Required]
    public decimal ValueAmount { get; set; }
}