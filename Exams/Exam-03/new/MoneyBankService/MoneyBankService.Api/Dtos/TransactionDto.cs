using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Api.Dtos
{
    public class TransactionDto
    {
        [Required] public int Id { get; set; }
        [Required][RegularExpression(@"\d{10}")] public string AccountNumber { get; set; } = null!;
        [Required] public decimal ValueAmount { get; set; }
    }
}
