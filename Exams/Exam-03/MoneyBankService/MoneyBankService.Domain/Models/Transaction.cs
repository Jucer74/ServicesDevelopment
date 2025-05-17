using System.ComponentModel.DataAnnotations;
using MoneyBankService.Domain.Common;

namespace MoneyBankService.Domain.Models
{

    public class Transaction : EntityBase
    {
        [Required] [MaxLength(10)] public string AccountNumber { get; set; } = null!;

        [Required] public decimal ValueAmount { get; set; }
    }
}