using System.ComponentModel.DataAnnotations;

namespace MoneyBankWeb_josoterad.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Valor es Requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a cero")]
        public decimal ValueAmount { get; set; }
    }
}
