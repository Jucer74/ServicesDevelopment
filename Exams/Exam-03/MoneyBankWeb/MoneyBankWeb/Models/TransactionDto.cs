using System.ComponentModel.DataAnnotations;

namespace MoneyBankWeb_josoterad.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el número de cuenta")]
        [MaxLength(10, ErrorMessage = "Máximo 10 caracteres")]
        [RegularExpression(@"\d{10}", ErrorMessage = "El número de cuenta debe contener exactamente 10 dígitos")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "El valor de la transacción es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public decimal ValueAmount { get; set; }
    }
}
