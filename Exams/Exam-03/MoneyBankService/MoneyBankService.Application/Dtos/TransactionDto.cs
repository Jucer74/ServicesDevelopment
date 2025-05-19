using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Application.Dtos
{
    public class TransactionDto
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal ValueAmount { get; set; }
    }
}
